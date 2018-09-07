using UnityEngine;
using System.Collections;

public class PathMove : MonoBehaviour {

    public Transform player;
    public float speed;
    public Renderer[] paths; // {right, left, up, down}
    public GameObject cameraTopDown, originalCamera;
	public float cost;
    public Transform exitLocation, winPosition;
    public Canvas ui;
    private bool won = false;
    private Vector3 startLocation;
    private bool _isPlaying;
    public bool isPlaying{
        get{return _isPlaying;}
        set{
            if(value)
            {
                originalCamera.SetActive(false);
                cameraTopDown.SetActive(true);
                ui.worldCamera = cameraTopDown.GetComponent<Camera>();
                _isPlaying = true;
            }
            else
            {
                cameraTopDown.SetActive(false);
                originalCamera.SetActive(true);
                ui.worldCamera = originalCamera.GetComponent<Camera>();
                originalCamera.transform.position = new Vector3(exitLocation.position.x, originalCamera.transform.position.y, exitLocation.position.z);
                _isPlaying = false;
                Reset();
            }
        }
    }


    void Reset()
    {
        cost = 4;
        foreach(GameObject path in GameObject.FindGameObjectsWithTag("Wall2")){
            path.GetComponent <Renderer>().material.color = Color.white;
        }
        player.position = startLocation;
    }

    void Start()
    {

        cost = 4;
        _isPlaying = false;
        startLocation = player.position;
    }

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Player")){
            GameManager.solved[2] = false;
            won = false;
            isPlaying = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if(col.CompareTag("Player")){
            isPlaying = false;
		}
	}

    void Won(){
        GameManager.tries[2] = (int)cost;  //NOTE TRIES[2] SIGNIFIES COST IN THAT GAME
        GameManager.solved[2] = true;
        isPlaying = false;
    }
    void Update () {
        if(!isPlaying || GetComponent<ShowInstructions>().instructions.activeSelf) return;
        if(Input.GetButtonDown("Reset"))
        {
            Reset();
        }
        if(Input.GetButtonDown("Exit"))
        {
            isPlaying = false;
        }
        if(Input.GetButtonDown("Horizontal")){
            if(paths[0] && paths[0].material.color == Color.white && Input.GetAxisRaw("Horizontal") > 0){
                paths[0].material.color = Color.red;
                paths[0] = null;
                player.Translate(transform.right * -speed);
                cost -= 2;
            }
            if(paths[1] && paths[1].material.color == Color.white && Input.GetAxisRaw("Horizontal") < 0){
                paths[1].material.color = Color.red;
                paths[1] = null;
                player.Translate(transform.right * speed);
                cost += 2;
            }
        }
        if(Input.GetButtonDown("Vertical")){
            if(paths[2] && paths[2].material.color == Color.white && Input.GetAxisRaw("Vertical") > 0){
                paths[2].material.color = Color.red;
                paths[2] = null;
                player.Translate(transform.forward * -speed);
                cost *= 2;
            }
            if(paths[3] && paths[3].material.color == Color.white && Input.GetAxisRaw("Vertical") < 0){
                paths[3].material.color = Color.red;
                paths[3] = null;
                player.Translate(transform.forward * speed);
                cost /= 2;
            }
        }
        if((winPosition.position - player.position).sqrMagnitude <0.1f && !won)
        {
            StartCoroutine(DelayAndWin());
            won = true;
        }
    }
    IEnumerator DelayAndWin()
    {
        yield return new WaitForSeconds(0.5f);
        Won();
    }

}
