using UnityEngine;

public class PathPlate : MonoBehaviour {

    public Collider colRight, colLeft, colUp, colDown;
    public PathMove script;

    void OnTriggerEnter (Collider col) {
        if(col == colRight)
            script.paths[0] = GetComponent<Renderer>();
        if(col == colLeft)
            script.paths[1] = GetComponent<Renderer>();
        if(col == colUp)
            script.paths[2] = GetComponent<Renderer>();
        if(col == colDown)
            script.paths[3] = GetComponent<Renderer>();
    }

}
