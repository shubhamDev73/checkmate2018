using UnityEngine;

public class LightRoom : MonoBehaviour {

	public Transform[] lights;

	private int tries = 0;

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
				GameManager.score += (30 - tries) * 2;
				GameManager.solved[3] = true;
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if(col.CompareTag("Player") && !GameManager.solved[3])
			tries++;
	}

}
