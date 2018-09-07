using UnityEngine;

public class ShowInstructions : MonoBehaviour {

	public int miniGame;
    public MonoBehaviour []scripts;
    public GameObject instructions;
	private bool shown = false;
    private bool canShow = false;

	void Update () {
		if(!GameManager.solved[miniGame - 1] && canShow &&Input.GetButtonDown("Instructions") && !instructions.activeSelf){
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
        FindObjectOfType<UI>().Display(miniGame, new string[] {"Instructions"}, new string[] {"I"});
        FindObjectOfType<UI>().showing = true;
    }

}
