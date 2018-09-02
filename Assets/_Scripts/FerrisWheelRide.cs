using UnityEngine;

public class FerrisWheelRide : MonoBehaviour {
    public GameObject []seats;
    public RideToggle script;
    public Transform player;
    public float maxSpeed, minSpeed;
    private float rotationSpeed;
    private int mainSeat;
    private bool _rideStart;
    private bool rideStart
    {
        set{
            _rideStart = value;
            if(value)
            {
                mainSeat = LowestSeat();
            }
        }
        get{return _rideStart;}
    }
	void Start () {
		_rideStart = false;
	}

    int LowestSeat()
    {
        float lowest = seats[0].transform.position.y;
        int min = 0;
        for(int i=1;i<seats.Length;i++)
        {
            if(seats[i].transform.position.y < lowest)
                min = i;
        }
        return min;
    }

	void FixedUpdate () {
        if(rideStart && script.onRide)
        {
            player.position = seats[mainSeat].transform.position - new Vector3(0, 0.5f, 0);
            rotationSpeed = Mathf.Lerp(rotationSpeed, maxSpeed, 0.05f);

        }
        else if(script.onRide)
        {
            rideStart = true;
        }
        else
        {
            rideStart = false;
            rotationSpeed = Mathf.Lerp(rotationSpeed, minSpeed, 0.01f);
        }
        transform.Rotate(0, 0, rotationSpeed, Space.Self);
	}
}
