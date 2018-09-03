using UnityEngine;

public class LightRoom : MonoBehaviour {

	public Transform[] lights;


	public static int tries = 0;

    int fib(int n){
        int a =0;
        int b =1;
        int temp = 0;
        for(int i=0; i<n;++i)
        {
            temp = b;
            b = b+a;
            a = temp;
        }
        return a;
    }
	void Update () {
		if(GameManager.solved[3])
			return;
		if(Input.GetButtonDown("Click")){
			bool done = true;
			foreach(Transform light in lights){
				if(!light.GetComponent<LightLamp>().correct){
					done = false;
					break;
				}
			}
			if(done){
				GameManager.score += (400 - 3*fib(tries));
				GameManager.solved[3] = true;
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if(!GameManager.solved[3] && col.CompareTag("Player"))
			tries++;
	}

}
