using System.Collections;
using UnityEngine;

public class Balance : MonoBehaviour {
	public GameObject coinPrefab;
    public int totalCoins;
	private int moveCoins = 1, tries = 0;
	private bool _chance = true;
    private int _coins;
    private bool canUpdate;
    private int coins
    {
        get{return _coins;}
        set{
            _coins = value;
            CalculateScale();
            transform.GetChild(0).eulerAngles = new Vector3(0, 0, (2 * _coins - totalCoins) * 0.5f);
            transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 180, 0);
            transform.GetChild(0).GetChild(1).eulerAngles = new Vector3(0, 180, 0);
            if(_coins == totalCoins / 2){
                if(chance){
                    GameManager.score += Mathf.Clamp(50 - tries, 0, 50);
                    GameManager.solved[4] = true;
                }else
                    Reset();
            }else if(_coins < totalCoins/2)
                Reset();

        }
    }
    private bool chance
    {
        get{return _chance;}
        set{
            _chance = value;
            if(!_chance)
            {
                StartCoroutine(Delay(2));
            }
        }
    }
	void Start () {
		for(int i = 0; i < totalCoins; i++) Instantiate(coinPrefab);
        canUpdate = false;
		Reset();
	}

	void Reset () {
        // add visual indicator that you have lost
        _chance =false;
		coins = totalCoins;
        CalculateScale();
		tries++;
        _chance = true;
	}
    void SkeleMove()
    {
        if((coins - totalCoins / 2) % 5 == 0) coins -= Random.Range(1, 5);
        else coins -= (coins - totalCoins / 2) % 5;
        chance = true;
    }
    void OnTriggerEnter(Collider col){
        if(!GameManager.solved[4] && col.CompareTag("Player")){
            canUpdate = true;
        }
    }
    void Update(){
        if(canUpdate && chance){
            if(Input.GetButtonDown("Increment")) moveCoins++;
            if(Input.GetButtonDown("Decrement")) moveCoins--;
            moveCoins = Mathf.Clamp(moveCoins, 1, 4);
            FindObjectOfType<UI>().DisplayText(moveCoins);
            if(Input.GetButtonDown("Click")){
                coins -= moveCoins;
                coins = Mathf.Clamp(coins, 0, totalCoins);
                chance = false;
            }
        }
    }
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        SkeleMove();
    }
	void OnTriggerExit (Collider col) {
		if(!GameManager.solved[4] && col.CompareTag("Player")){
			canUpdate = false;
			FindObjectOfType<UI>().DisplayText(0);
		}
	}
	void CalculateScale () {
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

    // IEnumerator PlayGame(float time)
    // {
    //     while(!GameManager.solved[4]){
    //         StartCoroutine(PlayerMove());
    //         SkeleMove();
    //         yield return new WaitForSeconds(time);
    //     }
    // }
    // IEnumerator PlayerMove()
    // {
    //     while(true){
    //         if(canIncrement){
    //             if(Input.GetButtonDown("Increment")) moveCoins++;
    //             if(Input.GetButtonDown("Decrement")) moveCoins--;
    //             moveCoins = Mathf.Clamp(moveCoins, 1, 4);
    //             FindObjectOfType<UI>().DisplayText(moveCoins);
    //             if(Input.GetButtonDown("Click")){
    //                 coins -= moveCoins;
    //                 coins = Mathf.Clamp(coins, 0, totalCoins);
    //                 return;
    //             }
    //         }
    //         yield return null;
    //     }
    // }
	// void OnTriggerEnter (Collider col) {
	// 	if(!GameManager.solved[4] && col.CompareTag("Player"))
	// 		canIncrement = true;
	// }
