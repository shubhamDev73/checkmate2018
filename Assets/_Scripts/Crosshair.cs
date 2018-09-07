using UnityEngine;

public class Crosshair : MonoBehaviour {

	void Update () {
        transform.position = Input.mousePosition;
	}
}
