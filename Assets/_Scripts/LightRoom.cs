using UnityEngine;

public class LightRoom : MonoBehaviour {

	public Transform[] lights;

	void OnTriggerEnter (Collider col) {
		if(col.CompareTag("Player"))
			LightSwitch.tries++;
	}

}
