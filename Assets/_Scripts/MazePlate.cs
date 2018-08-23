using UnityEngine;

public class MazePlate : MonoBehaviour {

	private static float cost = 0, lastCost = 0;
	private static Vector3 lastPlateLocation;
	private static bool updateXMode = false, updateYMode = false; // Considers whether to update only in y or x

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Player")){
			//Make Wall
			if(updateXMode)
				if(Vector3.Dot((transform.position - lastPlateLocation).normalized, transform.right) == 1)
					cost = lastCost - 2;
				else
					cost = lastCost + 2;
			
			if(updateYMode)
				if(Vector3.Dot((transform.position - lastPlateLocation).normalized, transform.forward) == 1)
					cost = lastCost * 2;
				else
					cost = lastCost / 2;
			
			Debug.Log("x: " + updateXMode.ToString() + ", y: " + updateYMode.ToString() + ", cost: " + cost.ToString());

			updateXMode = false;
			updateYMode = false;
		}
	}

	void OnTriggerExit (Collider col) {
		if(col.CompareTag("Player")){
			if(Mathf.Abs(Vector3.Dot((col.transform.position - transform.position).normalized, transform.right)) > Mathf.Abs(Vector3.Dot((col.transform.position - transform.position).normalized, transform.forward))){
				updateXMode = true;
				updateYMode = false;
			}else{
				updateXMode = false;
				updateYMode = true;
			}
			lastPlateLocation = transform.position;
			lastCost = cost;
			// Make Wall
		}
	}
	
}
