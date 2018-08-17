using UnityEngine;

public class Balance : MonoBehaviour {

	public int coins;

	private int moveCoins = 1;
	private int chance = 1;

	void OnTriggerStay (Collider col) {
		if(col.gameObject.tag == "Player"){
			moveCoins += (int)Mathf.Round(Input.GetAxis("Mouse ScrollWheel"));
			moveCoins = Mathf.Clamp(moveCoins, 1, 4);
			if(Input.GetButtonDown("Click") && chance == 1){
				coins -= moveCoins;
				chance = 0;
			}
		}
	}

	void Update () {
		if(chance == 1)
			return;
		// AI bot
	}

}
