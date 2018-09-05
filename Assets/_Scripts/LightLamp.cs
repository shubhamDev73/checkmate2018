using UnityEngine;

public class LightLamp : MonoBehaviour {

	[@HideInInspector]
	public int id;
	[@HideInInspector]
	public bool correct = false;

	private int settingId = 1;
	private bool canIncrement = false;

	void Update () {
		if(GameManager.solved[3] || !canIncrement) return;
		if(Input.GetButtonDown("Increment")) settingId++;
		if(Input.GetButtonDown("Decrement")) settingId--;
		if(settingId <= 0) settingId += 15;
		if(settingId >= 16) settingId -= 15;
	}

	void OnTriggerEnter (Collider col) {
		if(!GameManager.solved[3] && col.CompareTag("Player"))
			canIncrement = true;
	}

	void OnTriggerExit (Collider col) {
		if(!GameManager.solved[3] && col.CompareTag("Player")){
			canIncrement = false;
			FindObjectOfType<UI>().DisplayText(0);
		}
	}

	void OnTriggerStay (Collider col) {
		if(!GameManager.solved[3] && col.CompareTag("Player")){
			FindObjectOfType<UI>().DisplayText(settingId);
			if(Input.GetButtonDown("Click")){
				transform.GetChild(2).GetComponent<Renderer>().materials[1].SetTexture("_MainTex", Resources.Load<Texture2D>("Label3_" + settingId.ToString()));
				correct = settingId - 1 == id;
			}
		}
	}
}
