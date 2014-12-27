using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {
	public bool isTouchDown = false;
	enum Direction{LEFT, RIGHT, UP, DOWN, NONE};
	Direction dir;
	public int column, row;
	public bool isMove = false;
	static int destroyCount = 0;
	static string moveName = "";
	static string beMoveName = "";
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
			moveName = tempMove.tag;
			beMoveName = tempBeMove.tag;
			Match(Direction.LEFT);
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
			moveName = tempMove.tag;
			beMoveName = tempBeMove.tag;
			Match(Direction.RIGHT);
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
			moveName = tempMove.tag;
			beMoveName = tempBeMove.tag;
			Match(Direction.UP);
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
			moveName = tempMove.tag;
			beMoveName = tempBeMove.tag;
			Match(Direction.DOWN);
		}
	}

	void OnMouseDown() { 
		isTouchDown = true;
	}

	void OnMouseUp() {
		isTouchDown = false;
		isMove = false;
	}

	void Match(Direction tempDir) {
		GameObject[,] temp = Clone.puzzles;
		if(tempDir == Direction.DOWN) {
			GameObject tempMove = temp[column, row];
			GameObject tempBeMove = temp[column, row+1];
			if(column != 0 && column != Clone.ballNum-1) {
				if(temp[column, row].tag == temp[column-1, row].tag && temp[column+1, row].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column-1, row].SetActive(false);
					temp[column+1, row].SetActive(false);
				}
				if(tempBeMove.tag == temp[column-1, row+1].tag && temp[column+1, row+1].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[column-1, row+1].SetActive(false);
					temp[column+1, row+1].SetActive(false);
				}
			}
			if(column > 1) {
				if(temp[column, row].tag == temp[column-1, row].tag && temp[column-2, row].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column-1, row].SetActive(false);
					temp[column-2, row].SetActive(false);
				}
				if(tempBeMove.tag == temp[column-1, row+1].tag && temp[column-2, row+1].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[column-1, row+1].SetActive(false);
					temp[column-2, row+1].SetActive(false);
				}
			}
			if(column < Clone.ballNum-2) {
				if(temp[column, row].tag == temp[column+1, row].tag && temp[column+2, row].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column+1, row].SetActive(false);
					temp[column+2, row].SetActive(false);
				}
				if(tempBeMove.tag == temp[column+1, row+1].tag && temp[column+2, row+1].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[column+1, row+1].SetActive(false);
					temp[column+2, row+1].SetActive(false);
				}
			}
		}
		if(tempDir == Direction.UP) {

		}

//		for(int n = 0; n < Clone.ballNum; n++) {
//			for(int k = 0; k < Clone.ballNum; k++) {
//				TouchEvent eventF = (TouchEvent)Clone.puzzles[n, k].GetComponent("TouchEvent");
//				for(int i = 0; i < Clone.ballNum; i++) {
//					for(int j = 0; j < Clone.ballNum; j++) {
//						TouchEvent eventT = (TouchEvent)Clone.puzzles[i, j].GetComponent("TouchEvent");
//						if(eventT.column-1 == eventF.column && eventF.row == eventT.row && eventF.gameObject.tag == eventT.gameObject.tag) {
//							eventT.gameObject.SetActive(false);
//							eventF.gameObject.SetActive(false);
//							destroyCount++;
//							print ("F "+destroyCount);
//						}        
//					}
//				}
//			}
//		}
	}
}
