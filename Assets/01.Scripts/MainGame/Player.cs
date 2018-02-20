using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
            if (Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                _targetPosition = hitInfo.point;
                _stateMap[_stateType].UpdateInput();
            }
        }
    }
}
