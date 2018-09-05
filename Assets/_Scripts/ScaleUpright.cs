using UnityEngine;

public class ScaleUpright : MonoBehaviour {

	void LateUpdate () {
        transform.eulerAngles = new Vector3(0, 180, 0);
	}
}
