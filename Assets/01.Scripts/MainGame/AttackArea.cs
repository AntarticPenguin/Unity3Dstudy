using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    //Unity Functions

	void Start ()
    {
        Disable();
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("check: " + other);
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
