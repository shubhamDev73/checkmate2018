using UnityEngine;

public class CameraController : MonoBehaviour {

	public float maxAngleY;
	public float speed;

	void FixedUpdate () {
		transform.Translate(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed, Space.Self);

		transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
		transform.GetChild(0).Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
		float rot = transform.GetChild(0).eulerAngles.x;
		if(rot > 180 && rot < 360 - maxAngleY) rot = 360 - maxAngleY;
		if(rot < 180 && rot > maxAngleY) rot = maxAngleY;
		transform.GetChild(0).eulerAngles = new Vector3(rot, transform.GetChild(0).eulerAngles.y, transform.GetChild(0).eulerAngles.z);
	}

}
