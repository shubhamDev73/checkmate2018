using UnityEngine;

public class LightLamp : MonoBehaviour {

	[@HideInInspector]
	public int id;
	[@HideInInspector]
	public bool correct = false;

	private int settingId = 1;
	private bool canIncrement = false;

	void Update () {
		if(!canIncrement) return;
		if(Input.GetButtonDown("Increment")) settingId++;
		if(Input.GetButtonDown("Decrement")) settingId--;
		if(settingId <= 0) settingId += 15;
		if(settingId >= 16) settingId -= 15;
	}

	void OnTriggerEnter (Collider col) {
		if(col.CompareTag("Player"))
			canIncrement = true;
	}

	void OnTriggerExit (Collider col) {
		if(col.CompareTag("Player")){
			canIncrement = false;
			FindObjectOfType<UI>().DisplayText(0);
		}
	}

	void OnTriggerStay (Collider col) {
		if(col.CompareTag("Player")){
			FindObjectOfType<UI>().DisplayText(settingId);
			if(Input.GetButtonDown("Click"))
				correct = settingId == id;
		}
	}

}
