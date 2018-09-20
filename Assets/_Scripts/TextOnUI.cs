using UnityEngine;

public class TextOnUI : MonoBehaviour {

    public int miniGame, max;
    public UI ui;

    private bool canIncrement = false;

    void OnTriggerEnter (Collider col) {
        if(!GameManager.solved[miniGame] && col.CompareTag("Player"))
            canIncrement = true;
    }

    void OnTriggerExit (Collider col) {
        if(col.CompareTag("Player"))
            canIncrement = false;
    }

    void Update () {
        if(canIncrement && !ui.instructionsScreen.activeSelf && !GameManager.solved[miniGame]){
            if(miniGame < 10){
                if(!ui.gameElements.activeSelf){
                    if(Input.GetButtonDown("Click"))
                        ui.ShowGameElements(max, this);
                }else{
                    if(Input.GetButtonDown("Text"))
                        ui.ChangeText((int)Input.GetAxisRaw("Text"));
                    if(Input.GetButtonDown("Set"))
                        ui.HideGameElements();
                }
            }else{
                if(!ui.puzzleElements.activeSelf){
                    if(Input.GetButtonDown("Click"))
                        ui.ShowPuzzleElements(this);
                }else{
                    if(Input.GetButtonDown("Set"))
                        ui.HidePuzzleElements();
                }

            }
        }
    }

    public void Set () {
        int n = ui.GetText();
        string sol = ui.GetPuzzleText();
        if(miniGame > 10)
            GameManager.tries[miniGame]++;
        switch(miniGame){
            case 3:
                transform.GetChild(2).GetComponent<Renderer>().materials[1].SetTexture("_MainTex", Resources.Load<Texture2D>("Label3_" + n.ToString()));
                GetComponent<LightLamp>().correct = n - 1 == GetComponent<LightLamp>().id;
                break;
            case 4:
                if(GetComponent<Balance>().chance){
                    GetComponent<Balance>().coins -= n;
                    GetComponent<Balance>().chance = false;
                }
                break;
            case 11:
                if(sol.Trim().ToLower() == "i" && !GameManager.solved[11]){
                    GameManager.solved[11] = true;
                }
                break;
            case 12:
                if(sol.Trim().ToLower() == "what" && !GameManager.solved[12]){
                    GameManager.solved[12] = true;
                }
                break;
            case 13:
                if(sol.Trim().ToLower() == "v" && !GameManager.solved[13]){
                    GameManager.solved[13] = true;
                }
                break;
            case 14:
                if(sol.Trim().ToLower() == "f" && !GameManager.solved[14]){
                    GameManager.solved[14] = true;
                }
                break;
        }
    }

}
