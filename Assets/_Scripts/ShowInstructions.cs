using UnityEngine;

public class ShowInstructions : MonoBehaviour {

	public int miniGame;

	void OnTriggerEnter (Collider col) {
		if(col.gameObject.tag == "Player" && !GameManager.solved[miniGame]) FindObjectOfType<UI>().Display(miniGame);
	}

}
