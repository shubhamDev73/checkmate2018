using UnityEngine;

public class LightRoom : MonoBehaviour {

    public GameObject triesText;
	public Transform[] lights;
	public static int tries = 0;

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
                GameManager.tries[3] = tries;
				GameManager.solved[3] = true;

			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if(!GameManager.solved[3] && col.CompareTag("Player")){
			tries++;
            GameManager.tries[3] = tries;
            triesText.SetActive(true);
        }
	}

    void OnTriggerExit (Collider col) {
        if(col.CompareTag("Player"))
            triesText.SetActive(false);
    }

}
