using UnityEngine;

public class LightRoom : MonoBehaviour {

	public Transform[] lights;

	void OnTriggerEnter (Collider col) {
		LightSwitch.tries++;
	}

}
