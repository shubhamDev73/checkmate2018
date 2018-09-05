using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float maxAngleY;
	public float speed;
    private bool jumping, crouching;
    private bool falling = false;
    private float yVel = 0;
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !jumping){
            yVel = 0;
            falling = false;
            jumping = true;
        }
        if(Input.GetButton("Crouch") && !crouching)
        {
            crouching = true;
            transform.GetChild(0).Translate(0,-0.2f,0);
        }else if(!Input.GetButton("Crouch") && crouching)
        {
            crouching = false;
            transform.GetChild(0).Translate(0, +0.2f,0);
        }
    }

	void FixedUpdate () {
		transform.Translate(Input.GetAxis("Horizontal") * speed * (Input.GetButton("Sprint") ? 1.5f : 1), 0, Input.GetAxis("Vertical") * speed * (Input.GetButton("Sprint") ? 1.5f:1), Space.Self);
		transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
		transform.GetChild(0).Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
		float rot = transform.GetChild(0).eulerAngles.x;
		if(rot > 180 && rot < 360 - maxAngleY) rot = 360 - maxAngleY;
		if(rot < 180 && rot > maxAngleY) rot = maxAngleY;
		transform.GetChild(0).eulerAngles = new Vector3(rot, transform.GetChild(0).eulerAngles.y, transform.GetChild(0).eulerAngles.z);

        if(jumping)
        {
            if(falling){
                yVel -= 0.003f;
            }else if (yVel <= 0.05f){
                yVel += 0.01f;
            }
            transform.GetChild(0).Translate(0, yVel, 0, Space.Self);
            if(!falling && Mathf.Abs(transform.GetChild(0).position.y - transform.position.y) >= 0.4f){
                falling = true;
            }
            else if(falling && Mathf.Abs(transform.GetChild(0).position.y - transform.position.y) <= 0.06f){
                falling = false;
                transform.GetChild(0).position = transform.position;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                transform.GetChild(0).localEulerAngles = new Vector3(transform.GetChild(0).localEulerAngles.x, 0, 0);
                jumping = false;
                yVel = 0;
            }
        }
	}

}
