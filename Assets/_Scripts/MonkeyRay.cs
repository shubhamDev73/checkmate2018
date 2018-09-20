using UnityEngine;

public class MonkeyRay : MonoBehaviour {

    public GameObject crosshair;

    public bool canCast;

	void Update () {
        if(!GameManager.solved[1] && canCast && Input.GetButtonDown("Click"))// && !GetComponent<ShowInstructions>().instructions.activeSelf)
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
        if(!GameManager.solved[1] && col.CompareTag("Player")){
            canCast = true;
            crosshair.SetActive(true);
        }
    }
    void OnTriggerExit(Collider col){
        if(GameManager.solved[1] || col.CompareTag("Player")){
            canCast = false;
            crosshair.SetActive(false);
        }
    }

}
