using UnityEngine;
using System.Collections;

public class BullRide : MonoBehaviour {
    public string []choices;
    public Transform player;
    public RideToggle script;
    public float delay;
    public GameObject bullUI;
    private int nextChoice;
    private float currentDelay;
    private Vector3 initRot;
    private bool _rideStart;
    private bool rideStart
    {
        get {return _rideStart;}
        set {
            _rideStart = value;
            if(_rideStart){
                initRot = transform.localEulerAngles;
                currentDelay = delay;
                bullUI.SetActive(true);
            }
            else{
                bullUI.SetActive(false);
                transform.localEulerAngles = initRot;
                script.onRide = false;
            }
        }
    }
    IEnumerator BullGame()
    {
        float timeLeft = delay;
        timeLeft = getNextTime();
        while(timeLeft > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time*20/(timeLeft/delay + 0.3f), 150)-75);
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
        rideStart = false;

    }
    float getNextTime()
    {
        int temp = nextChoice;
        do{
            nextChoice = Random.Range(0, choices.Length);
        }while(nextChoice == temp);
        for(int i = 0; i < 4; i++){
            bullUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        bullUI.transform.GetChild(nextChoice).gameObject.SetActive(true);
        currentDelay /= 1.2f;
        currentDelay = Mathf.Clamp(currentDelay,0.5f,delay);
        if(currentDelay == 0.5f && !GameManager.solved[7])
        {
            GameManager.solved[7] = true;
        }
        return currentDelay;
    }
	void Update () {
        if(script.onRide && !rideStart){
            rideStart = true;
            StartCoroutine(BullGame());
            return;
        }
        if(Mathf.Approximately(transform.eulerAngles.z,0f))
        {
            transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(transform.eulerAngles.z,0,0.1f));
        }
	}

}
