using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    //Unity Functions

	void Start ()
    {
        _destination = transform.position;
	}
	
	void Update ()
    {		
        if(InputManager.Instance.IsMouseDown())
        {
            Vector3 mousePosition = InputManager.Instance.GetCursorPosition();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                MoveStart(hitInfo.point);
            }
        }

        MoveUpdate();
	}


    //Charcter Move

    Vector3 _destination;
    Vector3 _velocity = Vector3.zero;

    void MoveStart(Vector3 destination)
    {
        //목적지 세팅
        _destination = destination;

    }

    void MoveUpdate()
    {
        Vector3 direction = (_destination - transform.position).normalized;
        _velocity = direction * 6.0f;

        Vector3 snapGround = Vector3.zero;
        if (gameObject.GetComponent<CharacterController>().isGrounded)
            snapGround = Vector3.down;

        //목적지와 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(_destination, transform.position);
        if(0.5f < distance)
        {
            gameObject.GetComponent<CharacterController>().Move(_velocity * Time.deltaTime + snapGround);
        }
        
        //목적지까지의 거리와 방향을 구한다
        //현재 속도를 보관한다
        //목적지에 가까이 왔으면 도착
    }
}
