using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour {

	public Animator gateLeft, gateRight, skeleton;
	public Transform player;
	public AudioSource bgm;

	void OnTriggerEnter (Collider col) {
		if(col.CompareTag("Player")){
			gateLeft.SetBool("start", true);
			gateRight.SetBool("start", true);
		}
	}

	void OnTriggerExit (Collider col) {
		if(col.CompareTag("Player")){
			gateLeft.SetBool("start", false);
			gateRight.SetBool("start", false);
			skeleton.transform.position = player.position + new Vector3(0.5f + 0.5f * Input.GetAxis("Horizontal"), -player.position.y, 2 + 1 * Input.GetAxis("Vertical"));
			skeleton.SetBool("start", true);
			bgm.Play();
			StartCoroutine("ShowInstructions");
			Destroy(GetComponent("Collider"));
		}
	}

	IEnumerator ShowInstructions () {
		yield return new WaitForSeconds(0.25f);
		FindObjectOfType<UI>().Display(0);
	}

}
