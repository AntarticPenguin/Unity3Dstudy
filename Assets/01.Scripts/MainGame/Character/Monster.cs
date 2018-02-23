using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    override public void Init()
    {
        base.Init();
        _characterType = eCharacterType.MONSTER;
    }

    override protected void InitState()
    {
        base.InitState();

        State idleState = new WargIdleState();
        _stateMap[eState.IDLE] = idleState;
    }
}
