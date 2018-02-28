using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    override public void Init()
    {
        base.Init();
        _characterType = eCharacterType.MONSTER;

        _attackRange = 2.0f;
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

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.NameToLayer("Character") == other.gameObject.layer)
        {
            Character character = other.gameObject.GetComponent<Character>();
            if( eCharacterType.PLAYER == character.GetCharacterType())
            {
                _targetObject = other.gameObject;
                ChangeState(eState.CHASE);
            }
        }
    }
}
