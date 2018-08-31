using UnityEngine;

public class Coin : MonoBehaviour {

	public Transform scale;

	private Vector3 lastPos;

	public void Place () {
		if(scale.position == lastPos) return;
		float random = Random.value * 0.1f;
		Vector2 randomPos = Random.insideUnitCircle * random * 3;
		transform.position = scale.position - new Vector3(0, 1.3f - random, 0) + new Vector3(randomPos.x, 0, randomPos.y);
		lastPos = scale.position;
	}

}
