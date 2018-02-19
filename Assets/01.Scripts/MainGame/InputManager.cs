using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if(null == _instance)
            {
                _instance = new InputManager();
                _instance.Init();
            }
            return _instance;
        }
    }

    void Init()
    {
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (eButtonState.UP == _buttonState)
            {
                ButtonDown();
            }
            else if (eButtonState.DOWN == _buttonState)
            {
                ButtonHold();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ButtonUp();
        }
    }

    //Mouse Input
    
    enum eButtonState
    {
        DOWN,
        HOLD,
        UP,
    }
    eButtonState _buttonState = eButtonState.UP;

    void ButtonDown()
    {
        _buttonState = eButtonState.DOWN;
    }

    void ButtonHold()
    {
        _buttonState = eButtonState.HOLD;
    }

    void ButtonUp()
    {
        _buttonState = eButtonState.UP;
    }

    public bool IsMouseDown()
    {
        return (eButtonState.DOWN == _buttonState);
    }

    public bool IsMouseHold()
    {
        return (eButtonState.HOLD == _buttonState);
    }

    public Vector3 GetCursorPosition()
    {
        Vector3 position = Input.mousePosition;
        return position;
    }
}
