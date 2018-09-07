using System.Collections;
using UnityEngine;

public class Balance : MonoBehaviour {
	public GameObject coinPrefab;
    public int totalCoins;
	private int moveCoins = 1, tries = 0;
	private bool _chance = true;
    private int _coins;
    private bool skeleWon;
    private bool canUpdate;
    public int coins
    {
        get{return _coins;}
        set{
            _coins = value;
            StartCoroutine(CalculateScale());
        }
    }
    public bool chance
    {
        get{return _chance;}
        set{
            _chance = value;
            if(!GameManager.solved[4] && !_chance)
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
        _chance = false;
        skeleWon = false;
		coins = totalCoins;
        StartCoroutine(CalculateScale());
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
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        SkeleMove();
    }
	void OnTriggerExit (Collider col) {
		if(!GameManager.solved[4] && col.CompareTag("Player")){
			canUpdate = false;
		}
	}
	IEnumerator CalculateScale () {
		int n = 0;
        transform.GetChild(0).eulerAngles = new Vector3(0, 0, (2 * _coins - totalCoins) * 0.5f);
        transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 180, 0);
        transform.GetChild(0).GetChild(1).eulerAngles = new Vector3(0, 180, 0);
		foreach(GameObject coin in GameObject.FindGameObjectsWithTag("Coin")){
			if(n < coins) coin.GetComponent<Coin>().scale = transform.GetChild(0).GetChild(0);
			else coin.GetComponent<Coin>().scale = transform.GetChild(0).GetChild(1);
			n++;
		}
        if(_coins == totalCoins / 2){
            if(chance){
                GameManager.tries[4] = tries;
                GameManager.solved[4] = true;
            }else
                skeleWon = true;
        }else if(_coins < totalCoins/2)
            skeleWon= true;
        yield return new WaitForSeconds(2);
        if(skeleWon)
            Reset();
	}
}
