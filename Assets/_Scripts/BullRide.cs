using UnityEngine;
using System.Collections;

public class BullRide : MonoBehaviour {
    public string []choices;
    public Transform player;
    public RideToggle script;
    public float delay;
    private int nextChoice;
    private float currentDelay;
    private bool _rideStart;
    private bool rideStart
    {
        get {return _rideStart;}
        set {
            _rideStart = value;
            if(_rideStart){
                transform.rotation = Quaternion.identity;
                currentDelay = delay;
            }
            else{
                script.onRide = false;
            }
        }
    }
    IEnumerator BullGame()
    {
        float timeLeft = delay;
        while(timeLeft > 0)
        {
            Debug.Log(choices[nextChoice]);
            transform.rotation = Quaternion.Euler(0,0,2*Mathf.PingPong(Time.time,45)-45);
            player.position = transform.GetChild(0).position;
            if(Input.GetButtonDown(choices[nextChoice]))
            {
                timeLeft = getNextTime();
            }else
            {
                timeLeft -= Time.deltaTime;
            }
            yield return 0;
        }

    }
    float getNextTime()
    {
        nextChoice = Random.Range(0, choices.Length);
        currentDelay /= 2;
        return currentDelay;
    }
	void Update () {
        if(script.onRide){
            StartCoroutine(BullGame());
            rideStart = false;
            return;
        }
        if(Mathf.Approximately(transform.eulerAngles.z,0f))
        {
            transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(transform.eulerAngles.z,0,0.1f));
        }
	}

}
