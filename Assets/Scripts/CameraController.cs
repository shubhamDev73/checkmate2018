using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float sensitivity;
    public float maxAngleY;
    public float speed;
    private Vector2 camRotation;
    private Vector3 camPosition;
    private void controlUsingInput(){
        camRotation.x += Input.GetAxis("Mouse X")*sensitivity;
        camRotation.y -= Input.GetAxis("Mouse Y")*sensitivity;
        camRotation.x = Mathf.Repeat(camRotation.x,360);
        camRotation.y = Mathf.Clamp(camRotation.y,-maxAngleY,maxAngleY);
        transform.rotation = Quaternion.Euler(camRotation.y,camRotation.x,0);
        if (Input.GetButtonDown("Fire1")){
            onClick();
        }

        moveCamera();
        
    }
    private void moveCamera(){
        
        // Vector3 camMovement;
        camPosition += transform.forward*speed*Time.deltaTime*Input.GetAxis("Vertical");
        camPosition += transform.right*speed*Time.deltaTime*Input.GetAxis("Horizontal");
        camPosition.y = 1;
        transform.position = camPosition;
        // camMovement = transform.right*Input.GetAxis("Vertical")*speed*Time.deltaTime;
        // camMovement += transform.forward*Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        // transform.Translate(camMovement);
        // transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }
    private void onClick(){
        if(Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update(){}
    public void LateUpdate(){
        controlUsingInput();
    }
    public void Start(){
        camPosition = transform.position;
    }
}
