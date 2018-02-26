﻿using System.Collections;
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
        idleState.Init(this);
        _stateMap[eState.IDLE] = idleState;
    }

    public List<WayPoint> _wayPointList;

    override public void ArrivedDestination()
    {
        int index = Random.Range(0, _wayPointList.Count);
        _targetPosition = _wayPointList[index].GetPosition();
    }
}
