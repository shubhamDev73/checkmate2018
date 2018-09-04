using UnityEngine;

public class ShowInstructions : MonoBehaviour {

	public int miniGame;
    public MonoBehaviour []scripts;
	private bool shown = false;

	void OnTriggerStay (Collider col) {
		if(!GameManager.solved[miniGame - 1] && col.CompareTag("Player") && (!shown || Input.GetButton("Instructions"))){
			FindObjectOfType<UI>().Display(miniGame);
			shown = true;
		}
	}

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            foreach(MonoBehaviour script in scripts){
                script.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            foreach(MonoBehaviour script in scripts){
                script.enabled = false;
            }
        }
    }

}
