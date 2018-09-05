using UnityEngine;

public class Maze : MonoBehaviour {

	public Transform player, monkey, exitLocation;
	public float speed;
	public int maxMonkeyTurns;
    public bool isPlaying;
	public bool pCanMoveRight, pCanMoveLeft, pCanMoveUp, pCanMoveDown, mCanMoveRight, mCanMoveLeft, mCanMoveUp, mCanMoveDown;
    public GameObject cameraTopDown;
    public GameObject originalCamera;
    public MazeWin mazeWin;
	private bool chance;
    private bool buttonChangedX , buttonChangedY;
	private int monkeyTurnsLeft;
	private Vector3 playerInit, monkeyInit;
	void Start () {
		chance = true;
		playerInit = player.position;
		monkeyInit = monkey.position;
		monkeyTurnsLeft = maxMonkeyTurns;
        buttonChangedX = buttonChangedY = true;
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player") && !GameManager.solved[5]){
            setIsPlaying(true);
        }
    }

    public void setIsPlaying(bool value)
    {
        if(value)
        {
            originalCamera.SetActive(false);
            cameraTopDown.SetActive(true);
            isPlaying = true;
        }
        else
        {
            cameraTopDown.SetActive(false);
            originalCamera.SetActive(true);
            originalCamera.transform.position = new Vector3(exitLocation.position.x, originalCamera.transform.position.y, exitLocation.position.z);
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
        mazeWin.tries += 1;
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
        if(Input.GetAxisRaw("Horizontal") == 1 && pCanMoveRight && buttonChangedX){
			player.Translate(Vector3.right * speed);
			chance = false;
            buttonChangedX = false;
            return;
		}
		if(Input.GetAxisRaw("Horizontal") == -1 && pCanMoveLeft && buttonChangedX){
			player.Translate(Vector3.left * speed);
			chance = false;
            buttonChangedX = false;
            return;
		}
		if(Input.GetAxisRaw("Vertical") == 1 && pCanMoveUp && buttonChangedY){
			player.Translate(Vector3.forward * speed);
			chance = false;
            buttonChangedY = false;
            return;
		}
        if(Input.GetAxisRaw("Vertical") == -1 && pCanMoveDown && buttonChangedY){
			player.Translate(Vector3.back * speed);
			chance = false;
            buttonChangedY = false;
            return;
		}
        if(Input.GetAxisRaw("Horizontal") == 0)
            buttonChangedX = true;
        if(Input.GetAxisRaw("Vertical") == 0)
            buttonChangedY = true;


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
