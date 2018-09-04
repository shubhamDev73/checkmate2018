using System.Collections;
using UnityEngine;

public class MonkeyDoor : MonoBehaviour {

	public int doorId;
	public Animator anim;
	public GameObject skeletonPrefab;
    public Transform player;
	private static bool[] monkeyIsIn = {true, true, true, true, true, true};
	private static int size = 6;
	private static int tries = 0;
	private bool clicked = false;
    private bool canCast;
    private bool _isOpen;
    private bool playerNear;
    public bool isOpen
    {
        get{return _isOpen;}
        set{
            _isOpen = value;
            if(_isOpen)
            {
                anim.SetBool("open", true);
                if(WinCheck(doorId)){
                    monkeyIsIn[doorId] = false;
                    GameManager.solved[1] = true;
                    GameManager.score += 600* Mathf.Pow(0.98f,tries);
                    StartCoroutine(SpawnSkeleton(player.position));
                }
                StartCoroutine(CloseDelay());
            }
            else
            {
                anim.SetBool("open", false);
                MonkeyUpdate(doorId);
            }
        }
    }

	void Update () {
		if(monkeyIsIn[doorId])
			transform.GetChild(0).GetComponent<Light>().color = new Vector4(0, 1, 0, 1);
		else
			transform.GetChild(0).GetComponent<Light>().color = new Vector4(1, 0, 0, 1);
	}

    IEnumerator CloseDelay()
    {
        Debug.Log("Starting coroutine");
        yield return new WaitForSeconds(1);
        Debug.Log("Waiting for player to exit");
        while(playerNear){
            yield return null;
        }
        Debug.Log("Exiting Coroutine");
        isOpen = false;
    }
    void OnDisable()
    {
        isOpen = false;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
            playerNear = true;

    }
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
            playerNear = false;
    }
	// void OnTriggerStay (Collider col) {
	// 	if(!GameManager.solved[1] && !clicked && col.CompareTag("Player") && Input.GetButtonDown("Click")){
	// 		anim.SetBool("open", true);
	// 		clicked = true;
	// 		if(WinCheck(doorId)){
	// 			monkeyIsIn[doorId] = false;
	// 			GameManager.solved[1] = true;
	// 			GameManager.score += 600* Mathf.Pow(0.98f,tries);
	// 			StartCoroutine(SpawnSkeleton(col.transform.position));
	// 		}
	// 	}
	// }

	// void OnTriggerExit (Collider col) {
	// 	if(!GameManager.solved[1] && col.CompareTag("Player") && clicked){
	// 		anim.SetBool("open", false);
	// 		MonkeyUpdate(doorId);
	// 		clicked = false;
	// 	}
	// }



	bool WinCheck (int choice) {
		tries++;
		int index = -1;
		int left = 0;
		for(int i = 0; i < size; i++){
			if(monkeyIsIn[i]){
				index = i;
				left++;
			}
		}
		return (left == 1 && index == choice);
	}

    void Start (){
        playerNear = false;
    }
	void MonkeyUpdate (int guess) {
		bool[] temp = new bool[size];
		for(int i = 0; i < size; i++)
			temp[i] = false;

		monkeyIsIn[guess] = false;

		if(monkeyIsIn[0]){
			temp[1] = true;
			temp[0] = false;
		}
		if(monkeyIsIn[size - 1]){
			temp[size - 2] = true;
			temp[size - 1] = false;
		}
		for(int i = 1; i < size-1; i++){
			if(monkeyIsIn[i]){
				temp[i - 1] = true;
				temp[i + 1] = true;
			}
		}
		monkeyIsIn = temp;
	}

	IEnumerator SpawnSkeleton (Vector3 position) {
		yield return new WaitForSeconds(0.5f);
		GameObject skeleton = Instantiate(skeletonPrefab);
		skeleton.transform.position = new Vector3(position.x, -0.25f, transform.position.z - 2);
		Destroy(skeleton, 0.5f);
	}

}
