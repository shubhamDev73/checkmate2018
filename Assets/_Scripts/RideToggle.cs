using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideToggle : MonoBehaviour {
    public Transform exitLocation, player;
    public Collider my_col;
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
                my_col.enabled = false;

            }
            else
            {
                player.position = new Vector3(exitLocation.position.x,playerLastY,exitLocation.position.z);
                my_col.enabled = true;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Player") && Input.GetButtonDown("Click") && !onRide)
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
