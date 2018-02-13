using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Unity Functions

	void Start ()
    {
		
	}
	
	void Update ()
    {
        InputManager.Instance.Update();
	}
}
