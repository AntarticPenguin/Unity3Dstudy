using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    //Unity Functions

	void Start ()
    {
        
    }
	
	void Update ()
    {
		if(null != _lookTarget)
        {
            Vector3 startLookPosition = _lookTarget.GetPosition() + _offset;

            Vector3 relativePos = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0) * new Vector3(0, 0, -_distance);
            transform.position = startLookPosition + relativePos;

            Vector3 endLookPosition = _lookTarget.GetPosition() + _offset;
            transform.position = _lookTarget.GetTransform().TransformPoint(relativePos);
            transform.LookAt(endLookPosition);
        }
	}


    //Camera

    public Player _lookTarget = null;
    float _verticalAngle = 45.0f;
    float _horizontalAngle = 0.0f;
    float _distance = 5.0f;
    Vector3 _offset = new Vector3(0.0f, 1.5f, 0.0f);
}
