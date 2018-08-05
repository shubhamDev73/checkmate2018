using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public float sensitivity;
    public float maxAngleY;
    public float speed;

    private const float eps = 0.00001f;
    private Vector3 camRotation;
    private Vector3 camPosition;
    public void LateUpdate(){
        controlUsingInput();

    }
    public void Start(){
        camPosition = transform.position;
    }
    private void controlUsingInput(){
        if (Input.GetButtonDown("Fire1")){
            onClick();
        }

        moveCamera();
        rotateCamera();
    }
    
    private void rotateCamera(){
        camRotation.z = 0;
        camRotation.x -= Input.GetAxis("Mouse Y")*sensitivity;
        camRotation.y += Input.GetAxis("Mouse X")*sensitivity;
        camRotation.y = Mathf.Repeat(camRotation.y,360);
        camRotation.x = Mathf.Clamp(camRotation.x,-maxAngleY,maxAngleY);
        transform.eulerAngles = camRotation;
    }
    private void moveCamera(){
        float x,y;
        y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");
        camPosition += transform.forward*speed*Time.deltaTime*y;
        camPosition += transform.right*speed*Time.deltaTime*x;
        camPosition.y = 1;
        transform.position = camPosition;
    }
    private void onClick(){
        if(Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    
    
}
