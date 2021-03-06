﻿using System.Collections;
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
                                                         |1 << LayerMask.NameToLayer("HitArea")))
            {
                if(LayerMask.NameToLayer("Ground") == hitInfo.collider.gameObject.layer)
                {
                    _targetPosition = hitInfo.point;
                    _targetObject = null;
                    _isSetMovePosition = true;
                    //_stateMap[_stateType].UpdateInput();
                }
                
                if(LayerMask.NameToLayer("HitArea") == hitInfo.collider.gameObject.layer)
                {
                    HitArea hitDetector = hitInfo.collider.GetComponent<HitArea>();
                    Character character = hitDetector.GetCharacter();
                    switch (character.GetCharacterType())
                    {
                        case eCharacterType.MONSTER:
                            //적으로 파악 => 추적 state
                            _targetObject = hitInfo.collider.gameObject;
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

    override public void StopChase()
    {
        Debug.Log("Player: StopChase and ChangeState(IDLE)");
        ChangeState(eState.IDLE);
    }

    override public bool CanChase(float distance)
    {
        return true;
    }
}
