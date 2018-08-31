using UnityEngine;

public class MazeWall : MonoBehaviour {

	public Maze script;
	public Collider pColliderRight, pColliderLeft, pColliderUp, pColliderDown, mColliderRight, mColliderLeft, mColliderUp, mColliderDown;

	void OnTriggerEnter (Collider col) {
		if(col == pColliderRight)
			script.pCanMoveRight = false;
		if(col == pColliderLeft)
			script.pCanMoveLeft = false;
		if(col == pColliderUp)
			script.pCanMoveUp = false;
		if(col == pColliderDown)
			script.pCanMoveDown = false;

		if(col == mColliderRight)
			script.mCanMoveRight = false;
		if(col == mColliderLeft)
			script.mCanMoveLeft = false;
		if(col == mColliderUp)
			script.mCanMoveUp = false;
		if(col == mColliderDown)
			script.mCanMoveDown = false;
	}

	void OnTriggerExit (Collider col) {
		if(col == pColliderRight)
			script.pCanMoveRight = true;
		if(col == pColliderLeft)
			script.pCanMoveLeft = true;
		if(col == pColliderUp)
			script.pCanMoveUp = true;
		if(col == pColliderDown)
			script.pCanMoveDown = true;

		if(col == mColliderRight)
			script.mCanMoveRight = true;
		if(col == mColliderLeft)
			script.mCanMoveLeft = true;
		if(col == mColliderUp)
			script.mCanMoveUp = true;
		if(col == mColliderDown)
			script.mCanMoveDown = true;
	}
}
