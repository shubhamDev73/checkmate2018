using UnityEngine;

public class Balance : MonoBehaviour {

	public int coins;
	public GameObject coinPrefab;
	private int moveCoins = 1, totalCoins, tries = 0;
	private bool canIncrement = false, chance, calculatedFirstScale;

	void Start () {
		for(int i = 0; i < coins; i++) Instantiate(coinPrefab);
		totalCoins = coins;
		Reset();
	}

	void Reset () {
		foreach(GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
			coin.GetComponent<Coin>().scale = transform.GetChild(0).GetChild(0);
		chance = true;
		calculatedFirstScale = false;
		coins = totalCoins;
		tries++;
	}

	void Update () {
		if(GameManager.solved[4]) return;
		transform.GetChild(0).eulerAngles = new Vector3(0, 0, (2 * coins - totalCoins) * 0.5f);
		transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 180, 0);
		transform.GetChild(0).GetChild(1).eulerAngles = new Vector3(0, 180, 0);
		if(!calculatedFirstScale){
			CalculateScale();
			calculatedFirstScale = true;
		}

		if(coins == totalCoins / 2){
			if(!chance){
				GameManager.score += Mathf.Clamp(50 - tries, 0, 50);
				GameManager.solved[4] = true;
			}else
				Reset();
		}

		if(!GameManager.solved[4] && !chance){
			if((coins - totalCoins / 2) % 5 == 0) coins -= Random.Range(1, 5);
			else coins -= (coins - totalCoins / 2) % 5;
			CalculateScale();
			chance = true;
		}

		if(canIncrement){
			if(Input.GetButtonDown("Increment")) moveCoins++;
			if(Input.GetButtonDown("Decrement")) moveCoins--;
			moveCoins = Mathf.Clamp(moveCoins, 1, 4);
		}
	}

	void OnTriggerEnter (Collider col) {
		if(!GameManager.solved[4] && col.CompareTag("Player"))
			canIncrement = true;
	}

	void OnTriggerExit (Collider col) {
		if(!GameManager.solved[4] && col.CompareTag("Player")){
			canIncrement = false;
			FindObjectOfType<UI>().DisplayText(0);
		}
	}


	void OnTriggerStay (Collider col) {
		if(!GameManager.solved[4] && col.CompareTag("Player")){
			FindObjectOfType<UI>().DisplayText(moveCoins);
			if(Input.GetButtonDown("Click") && chance){
				if(col.transform.position.x < transform.position.x) coins -= moveCoins;
				else coins += moveCoins;
				coins = Mathf.Clamp(coins, 0, totalCoins);
				CalculateScale();
				chance = false;
			}
		}
	}

	void CalculateScale () {
		foreach(GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
			coin.GetComponent<Coin>().Place();
		int n = 0;
		foreach(GameObject coin in GameObject.FindGameObjectsWithTag("Coin")){
			if(n < coins) coin.GetComponent<Coin>().scale = transform.GetChild(0).GetChild(0);
			else coin.GetComponent<Coin>().scale = transform.GetChild(0).GetChild(1);
			n++;
		}
		transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials[3].SetTexture("_EmissionMap", Resources.Load<Texture2D>("Label3_" + coins.ToString()));
		transform.GetChild(0).GetChild(1).GetComponent<Renderer>().materials[3].SetTexture("_EmissionMap", Resources.Load<Texture2D>("Label3_" + (totalCoins - coins).ToString()));
	}

}
