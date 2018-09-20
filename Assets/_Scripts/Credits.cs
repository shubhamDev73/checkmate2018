using UnityEngine;

public class Credits : MonoBehaviour {

	void FixedUpdate () {
		if(transform.position.y < 1100)
            transform.Translate(0, 1, 0);
	}
}
