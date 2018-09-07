using UnityEngine;
using System;

public class RodCast : MonoBehaviour {
    public RodBalance equipped;
    public Transform player;
    public GameObject crosshair;
    bool _castAllowed = false;
    bool castAllowed{
        get{return _castAllowed;}
        set{
            _castAllowed = value;
            crosshair.SetActive(value);
        }
    }
    public RodBalance []rodArray;
    public int torque;
    int calculateRotationOfAllBalances()
    {
        int temp = 0;
        foreach(RodBalance rod in rodArray)
        {
            StartCoroutine(rod.calculateRotation());
            Debug.Log(rod.gameObject.name);
            temp +=rod.torque;
        }
        return temp;
    }
    void Update()
    {
        if(castAllowed && !GetComponent<ShowInstructions>().instructions.activeSelf){
            if(Input.GetButtonDown("Click"))
            {
                if(!equipped){
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out hit, 10, 1<<10)){
                        if(hit.collider.CompareTag("Hook") && hit.collider.GetComponent <Hook>().occupied)
                        {
                            equipped = hit.collider.GetComponent <Hook>().detach();
                            torque = calculateRotationOfAllBalances();
                            return;
                        }
                    }
                    if(!equipped && Physics.Raycast(ray, out hit, 10, 1<<11))
                    {
                        if(hit.collider.CompareTag("Weight") && hit.collider.GetComponent <RodBalance>().equipable)
                        {
                            equipped = hit.collider.GetComponent<RodBalance>();
                        }
                    }
                }
                else
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out hit, 10))
                    {
                        if(hit.collider.CompareTag("Hook") && !hit.collider.GetComponent <Hook>().occupied)
                        {
                            hit.collider.GetComponent <Hook>().attach(equipped);
                            equipped = null;
                            torque = calculateRotationOfAllBalances();
                        }
                    }
                }
            }
            else if(Input.GetButtonDown("Exit") && equipped)
            {
                equipped.transform.position = equipped.primaryLocation;
                equipped = null;
            }
            if(equipped){
                equipped.transform.position = player.position + player.transform.forward*3;
            }
        }

    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player"))
        {
            castAllowed = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            castAllowed = false;
        }
    }

    void OnDisable(){
        if(equipped){
            equipped.transform.position = equipped.primaryLocation;
            castAllowed = false;
        }
    }
}
