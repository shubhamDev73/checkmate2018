using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideToggle : MonoBehaviour {
    public Transform exitLocation, player;
    private bool _onRide;
    private float playerLastY;
    public bool onRide
    {
        get {return _onRide;}
        set
        {
            _onRide = value;
            if(_onRide)
            {
                playerLastY = player.position.y;
            }
            else
            {
                player.position = new Vector3(exitLocation.position.x,playerLastY,exitLocation.position.z);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("Entered");
        if(col.CompareTag("Player") && Input.GetButtonDown("Click"))
        {
            Debug.Log("Starting Ride");
            onRide = true;
        }
    }
	void Start () {
        _onRide = false;

	}


	void Update () {
        if(onRide)
        {
            if(Input.GetButtonDown("Exit"))
            {
                Debug.Log("Exiting");
                onRide = false;
                return;
            }
        }
	}
}
