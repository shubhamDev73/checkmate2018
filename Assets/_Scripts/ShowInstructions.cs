using UnityEngine;

public class ShowInstructions : MonoBehaviour {

	public int miniGame;
    public MonoBehaviour []scripts;
    public GameObject instructions;
    public Transform beacon;
	private bool shown = false;
    private bool canShow = false;

	void Update () {
		if(!GameManager.solved[miniGame] && canShow && Input.GetButtonDown("Instructions") && !instructions.activeSelf){
            Show();
		}
	}

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            canShow = true;
			if(!shown){
                Show();
                shown = true;
            }
            foreach(MonoBehaviour script in scripts){
                script.enabled = true;
            }
            if(miniGame < 10){
                beacon.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
                beacon.GetChild(0).GetChild(0).GetComponent<Light>().color = Color.yellow;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            canShow = false;
            foreach(MonoBehaviour script in scripts){
                script.enabled = false;
            }

        }
    }

    void Show () {
        if(miniGame < 10){
            switch(miniGame){
                case 2:
                case 5:
                    FindObjectOfType<UI>().Display(miniGame, new string[] {"Interact", "Reset", "Instructions"}, new string[] {"E", "R", "I"});
                    break;
                default:
                    FindObjectOfType<UI>().Display(miniGame, new string[] {"Interact", "Instructions"}, new string[] {"E", "I"});
                    break;
            }
        }else{
            FindObjectOfType<UI>().Display(miniGame, new string[] {"Answer", "Instructions"}, new string[] {"E", "I"});
        }
        FindObjectOfType<UI>().showing = true;
    }

}
