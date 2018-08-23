using UnityEngine;

public class Balance : MonoBehaviour {

	public int coins;

	private int moveCoins = 1;
	private int chance = 1;
	private bool canIncrement = false;

	void Update () {
		if(!canIncrement) return;
		if(Input.GetButtonDown("Increment")) moveCoins++;
		if(Input.GetButtonDown("Decrement")) moveCoins--;
		moveCoins = Mathf.Clamp(moveCoins, 1, 4);
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
			FindObjectOfType<UI>().DisplayText(moveCoins);
			if(Input.GetButtonDown("Click") && chance == 1){
				coins -= moveCoins;
				Debug.Log(coins);
				// chance = 0;
			}
		}
	}

	// void Update () {
	// 	if(chance == 1)
	// 		return;
	// 	// AI bot
	// }

}
