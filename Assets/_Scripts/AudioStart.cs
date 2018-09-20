using UnityEngine;

public class AudioStart : MonoBehaviour {

    void OnTriggerEnter (Collider col){
        if(col.CompareTag("Player"))
            GetComponent<AudioSource>().Play();
    }

    void OnTriggerExit (Collider col){
        if(col.CompareTag("Player"))
            GetComponent<AudioSource>().Stop();
    }

}
