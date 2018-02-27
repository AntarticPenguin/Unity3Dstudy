using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    Vector3 _destination;
    Vector3 _velocity = Vector3.zero;

    override public void Start()
    {
        _destination = _character.GetTargetPosition();
        _character.SetAnimationTrigger("MOVE");
    }

    override public void Stop()
    {
            
    }

    override public void Update()
    {
        //목적지까지의 거리와 방향을 구한다
        _destination.y = _character.GetPosition().y;        //높이를 같게 하여 x,z축이 회전하지 않도록 함
        Vector3 direction = (_destination - _character.GetPosition()).normalized;
        _velocity = direction * 6.0f;

        Vector3 snapGround = Vector3.zero;
        if(_character.IsGround())
            snapGround = Vector3.down;

        //목적지와 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(_destination, _character.GetPosition());
        if (_character.GetAttackRange() < distance)
        {
            _character.Rotate(direction);
            _character.Move(_velocity * Time.deltaTime + snapGround);
        }
        else
        {
            _character.ArrivedDestination();
        }
    }

    override public void UpdateInput()
    {
        _destination = _character.GetTargetPosition();
    }
}
