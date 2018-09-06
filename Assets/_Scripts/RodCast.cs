using UnityEngine;
using System;

public class RodCast : MonoBehaviour {
    public RodBalance equipped;
    public Transform player;
    public RodBalance []rodArray;
    public int torque;
    void Update()
    {
        if(Input.GetButtonDown("Click"))
        {
            if(!equipped){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 10, 1<<10)){
                    if(hit.collider.CompareTag("Hook") && hit.collider.GetComponent <Hook>().occupied)
                    {
                        equipped = hit.collider.GetComponent <Hook>().detach();
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
                    if(hit.collider.CompareTag("Hook"))
                    {
                        hit.collider.GetComponent <Hook>().attach(equipped);
                        equipped = null;
                        torque = 0;
                        foreach(RodBalance rod in rodArray){
                            torque +=Mathf.Abs(rod.torque);
                        }
                    }
                }
            }
        }
        if(equipped){
            equipped.transform.position = player.position + new Vector3(1, 0, 0);
        }

    }
}
