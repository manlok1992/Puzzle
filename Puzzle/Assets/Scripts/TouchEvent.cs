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
			}
			if(Input.GetAxis("Mouse Y") < -0.3) {
				//Down
				dir = Direction.DOWN;
			}
			if(Input.GetAxis("Mouse Y") > 0.3) {
				//Up
				dir = Direction.UP;
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
			print ("move "+gameObject.name);
			print ("be move "+tempObj.gameObject.name);
			gameObject.name = "ball "+column.ToString()+"-"+row.ToString();
			tempObj.gameObject.name = "ball "+tempObj.column.ToString()+"-"+tempObj.row.ToString();
			column--;
			tempObj.column++;
			GameObject temp = Clone.puzzles[column-1, row];
			Clone.puzzles[column-1, row] = gameObject;
			Clone.puzzles[column, row] = temp;
			isMove = true;
		}
	}

	
	void moveRight() {
		
	}
	
	void moveUp() {
		
	}
	
	void moveDown() {
		
	}

	void OnMouseDown() { 
		isTouchDown = true;
	}

	void OnMouseUp() {
		isTouchDown = false;
		isMove = false;
	}
}
