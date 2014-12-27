using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {
	public bool isTouchDown = false;
	enum Direction{LEFT, RIGHT, UP, DOWN, NONE};
	Direction dir;
	public int column, row;
	public bool isMove = false;
	// Use this for initialization
	void Start () {
		dir = Direction.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		if(isTouchDown) {
			if(Input.GetAxis("Mouse X") < -0.3) {
				//Left
				dir = Direction.LEFT;
				if(!isMove)
					moveLeft();
			}
			if(Input.GetAxis("Mouse X") > 0.3) {
				//Right
				dir = Direction.RIGHT;
				if(!isMove)
					moveRight();
			}
			if(Input.GetAxis("Mouse Y") < -0.3) {
				//Down
				dir = Direction.DOWN;
				if(!isMove)
					moveDown();
			}
			if(Input.GetAxis("Mouse Y") > 0.3) {
				//Up
				dir = Direction.UP;
				if(!isMove)
					moveUp();
			}
			if(Input.GetAxis("Mouse X") < 0.3 && Input.GetAxis("Mouse X") > -0.3 && 
			   Input.GetAxis("Mouse Y") < 0.3 && Input.GetAxis("Mouse Y") > -0.3) {
				dir = Direction.NONE;
			}
		}
	}
	
	public void moveLeft() {
		if(column != 0) {
			TouchEvent tempObj = (TouchEvent)Clone.puzzles[column-1, row].GetComponent("TouchEvent");
			Vector3 tempPos = gameObject.transform.position;
			gameObject.transform.position = Clone.puzzles[column-1, row].transform.position;
			Clone.puzzles[column-1, row].transform.position = tempPos;
			GameObject tempMove = gameObject;
			GameObject tempBeMove = tempObj.gameObject;
			column--;
			tempObj.column++;

			Clone.puzzles[column+1, row] = tempBeMove;
			Clone.puzzles[column, row] = tempMove;
			isMove = true;
		}
	}

	
	void moveRight() {
		if(column != Clone.ballNum-1) {
			TouchEvent tempObj = (TouchEvent)Clone.puzzles[column+1, row].GetComponent("TouchEvent");
			Vector3 tempPos = gameObject.transform.position;
			gameObject.transform.position = Clone.puzzles[column+1, row].transform.position;
			Clone.puzzles[column+1, row].transform.position = tempPos;
			GameObject tempMove = gameObject;
			GameObject tempBeMove = tempObj.gameObject;
			column++;
			tempObj.column--;
			
			Clone.puzzles[column-1, row] = tempBeMove;
			Clone.puzzles[column, row] = tempMove;
			isMove = true;
		}
	}
	
	void moveUp() {
		if(row != Clone.ballNum-1) {
			TouchEvent tempObj = (TouchEvent)Clone.puzzles[column, row+1].GetComponent("TouchEvent");
			Vector3 tempPos = gameObject.transform.position;
			gameObject.transform.position = Clone.puzzles[column, row+1].transform.position;
			Clone.puzzles[column, row+1].transform.position = tempPos;
			GameObject tempMove = gameObject;
			GameObject tempBeMove = tempObj.gameObject;
			row++;
			tempObj.row--;
			
			Clone.puzzles[column, row-1] = tempBeMove;
			Clone.puzzles[column, row] = tempMove;
			isMove = true;
		}
	}
	
	void moveDown() {
		if(row != 0) {
			TouchEvent tempObj = (TouchEvent)Clone.puzzles[column, row-1].GetComponent("TouchEvent");
			Vector3 tempPos = gameObject.transform.position;
			gameObject.transform.position = Clone.puzzles[column, row-1].transform.position;
			Clone.puzzles[column, row-1].transform.position = tempPos;
			GameObject tempMove = gameObject;
			GameObject tempBeMove = tempObj.gameObject;
			row--;
			tempObj.row++;
			
			Clone.puzzles[column, row+1] = tempBeMove;
			Clone.puzzles[column, row] = tempMove;
			isMove = true;
		}
	}

	void OnMouseDown() { 
		isTouchDown = true;
	}

	void OnMouseUp() {
		isTouchDown = false;
		isMove = false;
	}
}
