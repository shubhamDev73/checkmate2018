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
        if(canIncrement){
            if(Time.timeScale == 1){
                if(Input.GetButtonDown("Click"))
                    ui.ShowGameElements(max, this);
            }else{
                if(Input.GetButtonDown("Text"))
                    ui.ChangeText((int)Input.GetAxisRaw("Text"));
                if(Input.GetButtonDown("Set"))
                    ui.HideGameElements();
            }
        }
    }

    public void Set () {
        int n = ui.GetText();
        switch(miniGame){
            case 3:
                transform.GetChild(2).GetComponent<Renderer>().materials[1].SetTexture("_MainTex", Resources.Load<Texture2D>("Label3_" + n.ToString()));
                GetComponent<LightLamp>().correct = n - 1 == GetComponent<LightLamp>().id;
                break;
            case 4:
                if(GetComponent<Balance>().chance){
                    transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials[3].SetTexture("_EmissionMap", Resources.Load<Texture2D>("Label3_" + n.ToString()));
                    transform.GetChild(0).GetChild(1).GetComponent<Renderer>().materials[3].SetTexture("_EmissionMap", Resources.Load<Texture2D>("Label3_" + (GetComponent<Balance>().totalCoins - n).ToString()));
                    GetComponent<Balance>().coins = n;
                }
                break;
        }
    }

}
