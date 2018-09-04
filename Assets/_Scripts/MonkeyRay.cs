using UnityEngine;

public class MonkeyRay : MonoBehaviour {
    private bool canCast;
	void Start () {

	}

	void Update () {
        if(!GameManager.solved[1] && canCast && Input.GetButtonDown("Click"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 50))
            {
                if(hit.collider.CompareTag("MonkeyDoor"))
                {
                    hit.collider.GetComponent <MonkeyDoor>().isOpen =true;
                }
            }
        }
	}
    void OnTriggerEnter(Collider col){
        if(!GameManager.solved[1] && col.CompareTag("Player"))
            canCast = true;
    }
    void OnTriggerExit(Collider col){
        if(GameManager.solved[1] || col.CompareTag("Player"))
            canCast = false;
    }

}
