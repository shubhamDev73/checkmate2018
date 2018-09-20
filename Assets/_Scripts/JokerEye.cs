using UnityEngine;

public class JokerEye : MonoBehaviour {

    public Transform player;

	void Update () {
		transform.LookAt(player);
	}
}
