using UnityEngine;

public class LightRoom : MonoBehaviour {

	public Renderer[] lights;

	void OnTriggerEnter (Collider col) {
		LightSwitch.tries++;
	}

}
