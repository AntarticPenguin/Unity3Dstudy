using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum eCharacterType
    {
        NONE,
        MONSTER,
        PLAYER,
    }
    protected eCharacterType _characterType = eCharacterType.NONE;

    public GameObject _characterVisual;


    //UnityFunctions

    void Start ()
    {
        Init();
    }
	
	void Update ()
    {
        UpdateCharacter();	
	}

    virtual public void UpdateCharacter()
    {
        UpdateChangeState();
        _stateMap[_stateType].Update();
    }


    //State

    public enum eState
    {
        IDLE,
        MOVE,
        ATTACK,
        CHASE,
        PATROL,
    }

    protected Dictionary<eState, State> _stateMap = new Dictionary<eState, State>();
    protected eState _stateType = eState.IDLE;
    eState _nextStateType = eState.IDLE;

    virtual protected void InitState()
    {
        //데이터 주도 프로그래밍: 스크립트로 다 빼기
        State idleState = new IdleState();
        State moveState = new MoveState();
        State chaseState = new ChaseState();
        State attackState = new AttackState();
        State patrolState = new PatrolState();

        idleState.Init(this);
        moveState.Init(this);
        chaseState.Init(this);
        attackState.Init(this);
        patrolState.Init(this);

        _stateMap.Add(eState.IDLE, idleState);
        _stateMap.Add(eState.MOVE, moveState);
        _stateMap.Add(eState.CHASE, chaseState);
        _stateMap.Add(eState.ATTACK, attackState);
        _stateMap.Add(eState.PATROL, patrolState);
    }

    public void ChangeState(eState stateType)
    {
        _nextStateType = stateType;
    }

    void UpdateChangeState()
    {
        if (_nextStateType != _stateType)
        {
            _stateMap[_stateType].Stop();

            _stateType = _nextStateType;
            if (_stateMap.ContainsKey(_stateType))
                _stateMap[_stateType].Start();
            else
                Debug.LogError("Can't find state " + _stateType + " of " + gameObject.name);
        }
    }


    //Type

    virtual public void Init()
    {
        InitAttackInfo();
        InitHitAreaInfo();
        InitState();
    }

    public eCharacterType GetCharacterType()
    {
        return _characterType;
    }


    //Idle

    public float GetRefreshTime()
    {
        return 3.0f;
    }

    public void Patrol()
    {
        ChangeState(eState.PATROL);
    }


    //Move && Rotate

    protected Vector3 _targetPosition = Vector3.zero;
    protected GameObject _targetObject = null;
    protected float _speed = 6.0f;
    protected bool _isSetMovePosition = false;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetTargetPosition()
    {
        return _targetPosition;
    }

    public GameObject GetTargetObject()
    {
        return _targetObject;
    }

    public void SetTargetObject(GameObject targetObject)
    {
        _targetObject = targetObject;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public bool IsGround()
    {
        return gameObject.GetComponent<CharacterController>().isGrounded;
    }

    public void SetMovePosition(bool setMovePosition)
    {
        _isSetMovePosition = setMovePosition;
    }

    public bool IsSetMovePosition()
    {
        return _isSetMovePosition;
    }

    public void Move(Vector3 velocity)
    {
        gameObject.GetComponent<CharacterController>().Move(velocity);
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
    }

    virtual public void ArrivedDestination()
    {
        ChangeState(Character.eState.IDLE);
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360.0f * Time.deltaTime);
    }

    public Transform GetTransform()
    {
        return transform;
    }


    //Atttack

    protected float _attackRange = 1.0f;

    public float GetAttackRange()
    {
        return _attackRange;
    }

    //Attack Area

    AttackArea[] _attackArea;

    void InitAttackInfo()
    {
        _attackArea = GetComponentsInChildren<AttackArea>();
    }

    public void AttackStart()
    {
        for (int i = 0; i < _attackArea.Length; i++)
            _attackArea[i].Enable();
    }

    public void AttackEnd()
    {
        for (int i = 0; i < _attackArea.Length; i++)
            _attackArea[i].Disable();
    }

    
    //Hit Area

    void InitHitAreaInfo()
    {
        HitArea[] hitArea = GetComponentsInChildren<HitArea>();
        for(int i = 0; i < hitArea.Length; i++)
        {
            hitArea[i].Init(this);
        }
    }


    //Animation

    public void SetAnimationTrigger(string trigger)
    {
        _characterVisual.GetComponent<Animator>().SetTrigger(trigger);
    }

    public AnimationPlayer GetAnimationPlayer()
    {
        return _characterVisual.GetComponent<AnimationPlayer>();
    }


    //Chase
    virtual public void StopChase()
    {

    }

    virtual public bool CanChase(float distance)
    {
        if (5.0f < distance)
            return false;
        return true;
    }
}
