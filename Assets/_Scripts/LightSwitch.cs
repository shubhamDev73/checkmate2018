using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public LightRoom room;
	public int id;
	public static int tries = 0;

	private float initIntensity;

	void Start () {
		for(int i=0;i<room.lights.Length;i++){
			Renderer temp = room.lights[i];
			int random = Random.Range(0, room.lights.Length);
			room.lights[i] = room.lights[random];
			room.lights[random] = temp;
		}
	}

	void OnTriggerStay (Collider col) {
		if(col.gameObject.tag == "Player" && Input.GetButtonDown("Click")){
			if(room.lights[id].material.color == Color.black)
				room.lights[id].material.color = Color.white;
			else
				room.lights[id].material.color = Color.black;
		}
	}

}
