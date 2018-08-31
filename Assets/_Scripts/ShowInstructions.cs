using UnityEngine;

public class ShowInstructions : MonoBehaviour {

	public int miniGame;

	private bool shown = false;

	void OnTriggerStay (Collider col) {
		if(!GameManager.solved[miniGame - 1] && col.CompareTag("Player") && (!shown || Input.GetButton("Instructions"))){
			FindObjectOfType<UI>().Display(miniGame);
			shown = true;
		}
	}

}
