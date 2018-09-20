using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideToggle : MonoBehaviour {
    public Transform exitLocation, player;
    public Collider my_col;
    private bool canRide = false;
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
                playerLastY = 1;//player.position.y;
                my_col.enabled = false;

            }
            else
            {
                player.position = new Vector3(exitLocation.position.x,playerLastY,exitLocation.position.z);
                player.rotation = Quaternion.identity;
                player.GetChild(0).rotation = Quaternion.identity;
                my_col.enabled = true;
            }
        }
    }

    void OnTriggerEnter (Collider col) {
        if(col.CompareTag("Player"))
            canRide = true;
    }

    void OnTriggerExit (Collider col) {
        if(col.CompareTag("Player"))
            canRide = false;
    }

	void Start () {
        _onRide = false;

	}

	void Update () {
        if(canRide){
            if(Input.GetButtonDown("Click")){
                onRide = true;
                return;
            }
        }
        if((Input.GetButtonDown("Exit") || Input.GetButtonDown("Click")) && onRide)
            onRide = false;
	}
}
