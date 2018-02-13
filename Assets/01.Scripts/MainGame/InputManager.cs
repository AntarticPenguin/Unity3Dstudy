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
        if(Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if(Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    //Mouse Input

    bool _isMouseDown = false;

    void MouseDown()
    {
        _isMouseDown = true;
    }

    void MouseUp()
    {
        _isMouseDown = false;
    }

    public bool IsMouseDown()
    {
        return _isMouseDown;
    }

    public Vector3 GetCursorPosition()
    {
        Vector3 position = Input.mousePosition;
        return position;
    }
}
