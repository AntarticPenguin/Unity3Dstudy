using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    override public void Init()
    {
        base.Init();
        _characterType = eCharacterType.PLAYER;
    }

    override public void UpdateCharacter()
    {
        base.UpdateCharacter();
        UpdateInput();
    }

    void UpdateInput()
    {
        if (InputManager.Instance.IsMouseDown())
        {
            Vector3 mousePosition = InputManager.Instance.GetCursorPosition();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")
                                                         |1 << LayerMask.NameToLayer("Character")))
            {
                //지면 클릭
                if(LayerMask.NameToLayer("Ground") == hitInfo.collider.gameObject.layer)
                {
                    _targetPosition = hitInfo.point;
                    _stateMap[_stateType].UpdateInput();
                }
                
                //캐릭터 클릭
                if(LayerMask.NameToLayer("Character") == hitInfo.collider.gameObject.layer)
                {
                    HitDetector hitDetector = hitInfo.collider.GetComponent<HitDetector>();
                    Character character = hitDetector.GetCharacter();
                    switch (character.GetCharacterType())
                    {
                        case eCharacterType.MONSTER:
                            //적으로 파악 => 추적 state
                            _targetPosition = hitInfo.collider.transform.position;
                            ChangeState(eState.CHASE);
                            break;
                    }
                }
            }
        }

        //for test
        if(InputManager.Instance.IsAttackButtonDown())
        {
            ChangeState(eState.ATTACK);
        }
    }
}
