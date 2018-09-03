using UnityEngine;

public class DragonRide : MonoBehaviour {
    public RideToggle script;
    public Transform player;
    public float omega;
    public float thetaMax;
    private float tempTime;
    private bool _rideStart;
    private bool rideStart
    {
        set
        {
            _rideStart = value;
            tempTime = Time.time;
            if(_rideStart){
                transform.rotation = Quaternion.Euler(0, 0, thetaMax);
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
            transform.rotation = Quaternion.Euler(0, 0, thetaMax*Mathf.Cos(omega*(tempTime-Time.time)));
            player.position = transform.GetChild(0).position;
        }
        else
        {
            rideStart = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
	}
}
