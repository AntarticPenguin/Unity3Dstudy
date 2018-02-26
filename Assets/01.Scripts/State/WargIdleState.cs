using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargIdleState : IdleState
{
    float _waitTime = 0.0f;

    public override void Update()
    {
        //일정시간이 지나면 패트롤
        _waitTime += Time.deltaTime;
        if (_character.GetRefreshTime() <= _waitTime)
        {
            Debug.Log("Warg Starts Patrol");
            _character.Patrol();
            _waitTime = 0.0f;
        }
    }
}
