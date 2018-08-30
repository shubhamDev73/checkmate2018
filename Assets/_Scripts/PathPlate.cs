using UnityEngine;

public class PathPlate : MonoBehaviour {

	public GameObject wallPrefab;

	private static float cost = 0, lastCost = 0;
	private static Vector3 lastPlateLocation;
	private static bool updateXMode = false, updateYMode = false; // Considers whether to update only in y or x

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Player")){
			int rot = 180;
			col.gameObject.transform.position = new Vector3(transform.position.x, col.gameObject.transform.position.y, transform.position.z);
			if(updateXMode)
				if(Vector3.Dot((transform.position - lastPlateLocation).normalized, transform.right) == 1){
					cost = lastCost - 2;
					rot = 270;
				}else{
					cost = lastCost + 2;
					rot = 90;
				}
			
			if(updateYMode)
				if(Vector3.Dot((transform.position - lastPlateLocation).normalized, transform.forward) == 1){
					cost = lastCost * 2;
					rot = 180;
				}else{
					cost = lastCost / 2;
					rot = 0;
				}
			
			Debug.Log("x: " + updateXMode.ToString() + ", y: " + updateYMode.ToString() + ", cost: " + cost.ToString());
			Instantiate(wallPrefab, transform.position, Quaternion.Euler(0, rot, 0));

			updateXMode = false;
			updateYMode = false;
		}
	}

	void OnTriggerExit (Collider col) {
		if(col.CompareTag("Player")){
			int rot = 180;
			if(Mathf.Abs(Vector3.Dot((col.transform.position - transform.position).normalized, transform.right)) > Mathf.Abs(Vector3.Dot((col.transform.position - transform.position).normalized, transform.forward))){
				updateXMode = true;
				updateYMode = false;
				rot = 180 - ((int)Mathf.Sign(Vector3.Dot((col.transform.position - transform.position).normalized, transform.right)) * 90);
			}else{
				updateXMode = false;
				updateYMode = true;
				rot = 90 - ((int)Mathf.Sign(Vector3.Dot((col.transform.position - transform.position).normalized, transform.forward)) * 90);
			}
			lastPlateLocation = transform.position;
			lastCost = cost;
			Instantiate(wallPrefab, transform.position, Quaternion.Euler(0, rot, 0));
		}
	}
	
}
