using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    override public void Start()
    {
        _character.GetAnimationPlayer().Play("ATTACK", null,
        () =>
        {
            //공격 충돌체를 킨다
            _character.AttackStart();
        },
        () =>
        {
            //공격 충돌체를 끈다
            _character.AttackEnd();
            //_isCombo = true;
        },
        () =>
        {
            //_isCombo = false;
            _character.ChangeState(Character.eState.IDLE);
        });
    }

    override public void Update()
    {
        
    }
}
