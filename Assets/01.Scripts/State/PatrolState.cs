using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    List<Vector3> _wayPoints = new List<Vector3>();
    int _wayCount;

    override public void Start()
    {
        WayPoint[] wayPointObject = _character.GetWayPoints().GetComponentsInChildren<WayPoint>(); 
        for(int i = 0; i < wayPointObject.Length; i++)
        {
            _wayPoints.Add(wayPointObject[i].transform.position);
        }
        _wayCount = Random.Range(0, _wayPoints.Count);
        _character.SetAnimationTrigger("MOVE");
    }

    override public void Stop()
    {

    }

    override public void Update()
    {
        Debug.Log(_wayCount);
        Vector3 destination = _wayPoints[_wayCount];
        destination.y = _character.GetPosition().y;
        Vector3 direction = (destination - _character.GetPosition()).normalized;
        Vector3 velocity = direction * 4.0f;

        Vector3 snapGround = Vector3.zero;
        if (_character.IsGround())
            snapGround = Vector3.down;

        //목적지와 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(destination, _character.GetPosition());
        if (0.5f < distance)
        {
            _character.Rotate(direction);
            _character.Move(velocity * Time.deltaTime + snapGround);
        }
        else
        {
            _wayCount = Random.Range(0, _wayPoints.Count);
        }
    }

    override public void UpdateInput()
    {
        
    }
}
