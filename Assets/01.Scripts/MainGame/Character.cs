using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject _characterVisual;

    //UnityFunctions

    void Start ()
    {
        InitState();
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
    }

    protected Dictionary<eState, State> _stateMap = new Dictionary<eState, State>();
    protected eState _stateType = eState.IDLE;
    eState _nextStateType = eState.IDLE;

    void InitState()
    {
        State idleState = new IdleState();
        State moveState = new MoveState();

        idleState.Init(this);
        moveState.Init(this);

        _stateMap.Add(eState.IDLE, idleState);
        _stateMap.Add(eState.MOVE, moveState);
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


    //Animation

    public void SetAnimationTrigger(string trigger)
    {
        _characterVisual.GetComponent<Animator>().SetTrigger(trigger);
    }
}
