﻿using UnityEngine;

public class UpRide : MonoBehaviour {
    public RideToggle script;
    public Transform player;
    private bool goingUp;
    private float velocity;
    private bool _rideStart;
    private bool rideStart
    {
        set
        {
            _rideStart = value;
            transform.position = new Vector3(transform.position.x,1,transform.position.z);
            goingUp = true;
            if(!_rideStart)
            {
                velocity = 0f;
            }
        }
        get{return _rideStart;}
    }
	void Start () {
        _rideStart = false;
	}

	void FixedUpdate () {
        if(script.onRide)
        {
            if(goingUp)
            {
                velocity = 0.015f;
                transform.Rotate(0,0.15f,0,Space.Self);
                if(transform.position.y >= 10f)
                {
                    goingUp = false;
                }
            }else
            {
                velocity -= 0.003f;
                if(transform.position.y <= 1f)
                {
                    goingUp = true;
                }
            }
            player.position = transform.GetChild(0).position;
        }
        else
        {
            rideStart = false;
        }
        transform.Translate(0, velocity, 0);
	}
}
