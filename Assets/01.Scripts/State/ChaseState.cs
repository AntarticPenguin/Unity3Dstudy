using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Vector3 _velocity = Vector3.zero;

    override public void Start()
    {
        _character.SetAnimationTrigger("MOVE");
    }

    override public void Stop()
    {

    }

    override public void Update()
    {
        //목적지까지의 거리와 방향을 구한다
        Vector3 destination = _character.GetTargetPosition();
        destination.y = _character.GetPosition().y;        //높이를 같게 하여 x,z축이 회전하지 않도록 함
        Vector3 direction = (destination - _character.GetPosition()).normalized;
        _velocity = direction * 6.0f;

        Vector3 snapGround = Vector3.zero;
        if (_character.IsGround())
            snapGround = Vector3.down;

        //목적지와 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(destination, _character.GetPosition());
        if (0.5f < distance)
        {
            _character.Rotate(direction);
            _character.Move(_velocity * Time.deltaTime + snapGround);
        }
        else
        {
            _character.ChangeState(Character.eState.ATTACK);
        }
    }

    override public void UpdateInput()
    {
        
    }
}