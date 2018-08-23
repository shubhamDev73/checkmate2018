using UnityEngine;

public class ShowInstructions : MonoBehaviour {

	public int miniGame;

	private bool shown = false;

	void OnTriggerStay (Collider col) {
		if(col.gameObject.tag == "Player" && !GameManager.solved[miniGame - 1] && (!shown || Input.GetButton("Instructions"))){
			FindObjectOfType<UI>().Display(miniGame);
			shown = true;
		}
	}

}
