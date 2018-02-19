using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject _characterVisual;

    //Unity Functions

    void Start ()
    {
        InitState();
	}

    void Update()
    {
        UpdateChangeState();
        UpdateInput();
        _stateMap[_stateType].Update();
    }

    void UpdateInput()
    {
        if (InputManager.Instance.IsMouseDown())
        {
            Vector3 mousePosition = InputManager.Instance.GetCursorPosition();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                _targetPosition = hitInfo.point;
                _stateMap[_stateType].UpdateInput();
            }
        }
    }


    //State

    public enum eState
    {
        IDLE,
        MOVE,
    }
    eState _stateType = eState.IDLE;
    eState _nextStateType = eState.IDLE;

    Dictionary<eState, State> _stateMap = new Dictionary<eState, State>();

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

    Vector3 _targetPosition = Vector3.zero;

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
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360.0f * Time.deltaTime);
    }


    //Animation
        
    public void SetAnimationTrigger(string trigger)
    {
        _characterVisual.GetComponent<Animator>().SetTrigger(trigger);
    }
}
