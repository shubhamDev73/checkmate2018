using UnityEngine;

public class MonkeyDoor : MonoBehaviour {

	public int doorId;
	public Animator anim;

	private static bool[] monkeyIsIn = {true, true, true, true, true, true};
	private static int size = 6;
	private static int tries = 0;
	private bool clicked = false;

	void Update () {
		if(monkeyIsIn[doorId])
			transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
		else
			transform.GetChild(0).GetComponent<Renderer>().material.color = Color.grey;
	}

	void OnTriggerStay (Collider col) {
		if(!clicked && col.gameObject.name == "Fake Camera" && Input.GetButtonDown("Click")){
			anim.SetBool("open", true);
			clicked = true;
			if(WinCheck(doorId)){
				Debug.Log("Wow You Found Me!!!");
				Debug.Log("It took " + tries.ToString() + " tries");
				monkeyIsIn[doorId]= false;
			}
		}
	}

	void OnTriggerExit (Collider col) {
		if(clicked){
			anim.SetBool("open", false);
			MonkeyUpdate(doorId);
			clicked = false;
		}
	}

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

}
