using UnityEngine;

public class Balance : MonoBehaviour {

	public int coins;

	private int moveCoins = 1, totalCoins;
	private bool canIncrement = false, chance = true;

	void Start () {
		totalCoins = coins;
	}

	void Update () {
		transform.GetChild(0).eulerAngles = new Vector3(0, 0, (2 * coins - totalCoins) * 0.5f);
		transform.GetChild(0).GetChild(0).eulerAngles = Vector3.zero;
		transform.GetChild(0).GetChild(1).eulerAngles = Vector3.zero;
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
			if(Input.GetButtonDown("Click") && chance){
				coins -= moveCoins;
				Debug.Log(coins);
				// chance = false;
			}
		}
	}

	// void Update () {
	// 	if(chance)
	// 		return;
	// 	// AI bot
	// }

}
