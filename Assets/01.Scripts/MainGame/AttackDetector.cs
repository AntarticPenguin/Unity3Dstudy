using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    //Unity Functions

	void Start ()
    {
        Disable();
	}
	
	void Update ()
    {
		
	}


    //Collider on/off

    public void Enable()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<Collider>().enabled = false;
    }
}
