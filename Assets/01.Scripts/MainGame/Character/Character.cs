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
        State idleState = new IdleState();
        State moveState = new MoveState();
        State chaseState = new ChaseState();
        State attackState = new AttackState();

        idleState.Init(this);
        moveState.Init(this);
        chaseState.Init(this);
        attackState.Init(this);

        _stateMap.Add(eState.IDLE, idleState);
        _stateMap.Add(eState.MOVE, moveState);
        _stateMap.Add(eState.CHASE, chaseState);
        _stateMap.Add(eState.ATTACK, attackState);
    }

    public void ChangeState(eState stateType)
    {
        _nextStateType = stateType;
    }

    void UpdateChangeState()
    {
        if (_nextStateType != _stateType)
        {
            _stateType = _nextStateType;
            _stateMap[_stateType].Start();
        }
    }


    //Type

    virtual public void Init()
    {
        InitAttackInfo();
        InitHitDetectorInfo();
        InitState();
    }

    public eCharacterType GetCharacterType()
    {
        return _characterType;
    }


    //Idle

    public float GetRefreshTime()
    {
        return 5.0f;
    }

    public void Patrol()
    {
        ChangeState(eState.PATROL);
    }


    //Move && Rotate

    protected Vector3 _targetPosition = Vector3.zero;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetTargetPosition()
    {
        return _targetPosition;
    }

    public bool IsGround()
    {
        return gameObject.GetComponent<CharacterController>().isGrounded;
    }

    public void Move(Vector3 velocity)
    {
        gameObject.GetComponent<CharacterController>().Move(velocity);
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
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


    //Attack Detector

    AttackDetector[] _attackDetectors;

    void InitAttackInfo()
    {
        _attackDetectors = GetComponentsInChildren<AttackDetector>();
    }

    public void AttackStart()
    {
        for (int i = 0; i < _attackDetectors.Length; i++)
            _attackDetectors[i].Enable();
    }

    public void AttackEnd()
    {
        for (int i = 0; i < _attackDetectors.Length; i++)
            _attackDetectors[i].Disable();
    }

    
    //Hit Detector

    void InitHitDetectorInfo()
    {
        HitDetector[] hitDetectors = GetComponentsInChildren<HitDetector>();
        for(int i = 0; i < hitDetectors.Length; i++)
        {
            hitDetectors[i].Init(this);
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
}
