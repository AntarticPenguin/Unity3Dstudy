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

    override public void Update()
    {
        if (_character.IsSetMovePosition())
        {
            _character.ChangeState(Character.eState.MOVE);
            return;
        }
        if (null == _character.GetTargetObject())
        {
            _character.StopChase();
            return;
        }

        //목적지까지의 거리와 방향을 구한다
        Vector3 destination = _character.GetTargetObject().transform.position;
        destination.y = _character.GetPosition().y;        //높이를 같게 하여 x,z축이 회전하지 않도록 함
        Vector3 direction = (destination - _character.GetPosition()).normalized;
        _velocity = direction * _character.GetSpeed();

        Vector3 snapGround = Vector3.zero;
        if (_character.IsGround())
            snapGround = Vector3.down;

        //목적지와 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(destination, _character.GetPosition());
        if (distance < _character.GetAttackRange())
        {
            _character.ChangeState(Character.eState.ATTACK);
        }
        //else if(10.0f < distance) 캐릭터는 타겟을 유지하고 싶음 -> 조건을 위임하자.
        else if (false == _character.CanChase(distance))
        {
            _character.SetTargetObject(null);
        }
        else
        {
            _character.Rotate(direction);
            _character.Move(_velocity * Time.deltaTime + snapGround);
        }
    }

}