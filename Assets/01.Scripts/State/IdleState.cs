﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    override public void Start()
    {
        _character.SetAnimationTrigger("IDLE");
    }

    override public void Stop()
    {
    }

    override public void Update()
    {
    }

    override public void UpdateInput()
    {
        _character.ChangeState(Character.eState.MOVE);
    }
}
