using UnityEngine;

public class PathBoundary : MonoBehaviour {

	public GameObject minimap;

	void OnTriggerEnter (Collider col) {
		if(col.CompareTag("Player")){
			minimap.SetActive(true);
		}
	}

	void OnTriggerExit (Collider col) {
		if(col.CompareTag("Player")){
			minimap.SetActive(false);
		}
	}

}
