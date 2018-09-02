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
        if(col.CompareTag("Player") && Input.GetButtonDown("Click"))
        {
            onRide = true;
            return;
        }
    }
	void Start () {
        _onRide = false;
	}

	void LateUpdate () {
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
