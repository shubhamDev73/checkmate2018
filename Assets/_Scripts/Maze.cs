using UnityEngine;

public class Maze : MonoBehaviour {

	public Transform player, monkey;
	public float speed;
	public int maxMonkeyTurns;
    public bool isPlaying;
	public bool pCanMoveRight, pCanMoveLeft, pCanMoveUp, pCanMoveDown, mCanMoveRight, mCanMoveLeft, mCanMoveUp, mCanMoveDown;
    public GameObject camera;
    public GameObject mainCamera;
	private bool chance;
	private int monkeyTurnsLeft;
	private Vector3 playerInit, monkeyInit;
	void Start () {
		chance = true;
		pCanMoveRight = pCanMoveLeft = pCanMoveUp = pCanMoveDown = true;
		mCanMoveRight = mCanMoveLeft = mCanMoveUp = mCanMoveDown = true;
		playerInit = player.position;
		monkeyInit = monkey.position;
		monkeyTurnsLeft = maxMonkeyTurns;
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player")){
            setIsPlaying(true);
        }
    }

    public void setIsPlaying(bool value)
    {
        if(value)
        {
            Debug.Log("Entered");
            mainCamera.SetActive(false);
            camera.SetActive(true);
            isPlaying = true;
        }
        else
        {
            camera.SetActive(false);
            mainCamera.SetActive(true);
            isPlaying = false;
            reset();
        }
    }
    void monkeyCaught()
    {
        reset();

    }
    void reset()
    {
        player.position = playerInit;
        monkey.position = monkeyInit;
        Start();
    }
	void Update () {
        if(isPlaying){
            if(Input.GetButtonDown("Reset")){
                reset();
                return;
            }
            if(Input.GetButtonDown("Exit")){
                setIsPlaying(false);
                return;
            }
            if(Input.GetButtonDown("Submit")){
                chance = false;
                return;
            }
            if(chance){
                PlayerMovement();
            }
            else{
                MonkeyMovement();
                monkeyTurnsLeft--;
                if(monkeyTurnsLeft <= 0){
                    monkeyTurnsLeft = maxMonkeyTurns;
                    chance = true;
                }
            }

            if((player.position-monkey.position).sqrMagnitude<0.001f)
            {
                monkeyCaught();
                return;
            }
        }
	}

	void PlayerMovement () {
		if(Input.GetAxis("Horizontal") > 0 && pCanMoveRight){
			player.Translate(Vector3.right * speed);
			chance = false;
            return;
		}
		if(Input.GetAxis("Horizontal") < 0 && pCanMoveLeft){
			player.Translate(Vector3.left * speed);
			chance = false;
            return;
		}
		if(Input.GetAxis("Vertical") > 0 && pCanMoveUp){
			player.Translate(Vector3.forward * speed);
			chance = false;
            return;
		}
		if(Input.GetAxis("Vertical") < 0 && pCanMoveDown){
			player.Translate(Vector3.back * speed);
			chance = false;
            return;
		}
	}

	void MonkeyMovement () {
		if(
		Mathf.Abs(player.position.x - monkey.position.x - speed) < Mathf.Abs(player.position.x - monkey.position.x)
		||
		Mathf.Abs(player.position.x - monkey.position.x + speed) < Mathf.Abs(player.position.x - monkey.position.x)
		){
			if(player.position.x - monkey.position.x > 0 && mCanMoveRight){
				monkey.Translate(Vector3.right * speed);
				return;
			}
			if(player.position.x - monkey.position.x < 0 && mCanMoveLeft){
				monkey.Translate(Vector3.left * speed);
				return;
			}
		}
		if(
		Mathf.Abs(player.position.z - monkey.position.z - speed) < Mathf.Abs(player.position.z - monkey.position.z)
		||
 		Mathf.Abs(player.position.z - monkey.position.z + speed) < Mathf.Abs(player.position.z - monkey.position.z)
		){
			if(player.position.z - monkey.position.z > 0 && mCanMoveUp){
				monkey.Translate(Vector3.forward * speed);
				return;
			}
			if(player.position.z - monkey.position.z < 0 && mCanMoveDown){
				monkey.Translate(Vector3.back * speed);
				return;
			}
		}
	}
}
