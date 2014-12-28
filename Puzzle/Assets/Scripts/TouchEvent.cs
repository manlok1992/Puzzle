﻿using UnityEngine;
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
		if(tempDir == Direction.DOWN || tempDir == Direction.UP) {
			GameObject tempMove = temp[column, row];
			GameObject tempBeMove = null;
			int tempRow = 0;
			if(tempDir == Direction.DOWN) {
				tempBeMove = temp[column, row+1];
				tempRow = row+1;
			}
			else if(tempDir == Direction.UP) {
				tempBeMove = temp[column, row-1];
				tempRow = row-1;
			}
			if(column != 0 && column != Clone.ballNum-1) {
				if(temp[column, row].tag == temp[column-1, row].tag && temp[column+1, row].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column-1, row].SetActive(false);
					temp[column+1, row].SetActive(false);
				}
				else if(tempBeMove.tag == temp[column-1, tempRow].tag && temp[column+1, tempRow].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[column-1, tempRow].SetActive(false);
					temp[column+1, tempRow].SetActive(false);
				}
			}
			if(column > 1) {
				if(temp[column, row].tag == temp[column-1, row].tag && temp[column-2, row].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column-1, row].SetActive(false);
					temp[column-2, row].SetActive(false);
				}
				else if(tempBeMove.tag == temp[column-1, tempRow].tag && temp[column-2, tempRow].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[column-1, tempRow].SetActive(false);
					temp[column-2, tempRow].SetActive(false);
				}
			}
			if(column < Clone.ballNum-2) {
				if(temp[column, row].tag == temp[column+1, row].tag && temp[column+2, row].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column+1, row].SetActive(false);
					temp[column+2, row].SetActive(false);
				}
				else if(tempBeMove.tag == temp[column+1, tempRow].tag && temp[column+2, tempRow].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[column+1, tempRow].SetActive(false);
					temp[column+2, tempRow].SetActive(false);
				}
			}
			
			if(row > 1) { 
				if(temp[column, row].tag == temp[column, row-1].tag && temp[column, row].tag == temp[column, row-2].tag) {
					temp[column, row].SetActive(false);
					temp[column, row-1].SetActive(false);
					temp[column, row-2].SetActive(false);
				}
				else if(tempBeMove.tag == temp[column, tempRow-1].tag && tempBeMove.tag == temp[column, tempRow-2].tag) {
					tempBeMove.SetActive(false);
					temp[column, tempRow-1].SetActive(false);
					temp[column, tempRow-2].SetActive(false);
				}
			}
			if(row < Clone.ballNum-2) {
				if(temp[column, row].tag == temp[column, row+1].tag && temp[column, row].tag == temp[column, row+2].tag) {
					temp[column, row].SetActive(false);
					temp[column, row+1].SetActive(false);
					temp[column, row+2].SetActive(false);
				}
				else if(tempBeMove.tag == temp[column, tempRow+1].tag && tempBeMove.tag == temp[column, tempRow+2].tag) {
					tempBeMove.SetActive(false);
					temp[column, tempRow+1].SetActive(false);
					temp[column, tempRow+2].SetActive(false);
				}
			}
		}
		if(tempDir == Direction.LEFT || tempDir == Direction.RIGHT) {
			GameObject tempMove = temp[column, row];
			GameObject tempBeMove = null;
			int tempColumn = 0;
			if(tempDir == Direction.LEFT) {
				tempBeMove = temp[column+1, row];
				tempColumn = column+1;
			}
			else if(tempDir == Direction.RIGHT) {
				tempBeMove = temp[column-1, row];
				tempColumn = column-1;
			}
			if(row != 0 && row != Clone.ballNum-1) {
				if(temp[column, row].tag == temp[column, row-1].tag && temp[column, row+1].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column, row-1].SetActive(false);
					temp[column, row+1].SetActive(false);
				}
				else if(tempBeMove.tag == temp[tempColumn, row-1].tag && temp[tempColumn, row+1].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[tempColumn, row-1].SetActive(false);
					temp[tempColumn, row+1].SetActive(false);
				}
			}
			if(row > 1) {
				if(temp[column, row].tag == temp[column, row-1].tag && temp[column, row-2].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column, row-1].SetActive(false);
					temp[column, row-2].SetActive(false);
				}
				else if(tempBeMove.tag == temp[tempColumn, row-1].tag && temp[tempColumn, row-2].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[tempColumn, row-1].SetActive(false);
					temp[tempColumn, row-2].SetActive(false);
				}
			}
			if(row < Clone.ballNum-2) {
				if(temp[column, row].tag == temp[column, row+1].tag && temp[column, row+2].tag == temp[column, row].tag) {
					temp[column, row].SetActive(false);
					temp[column, row+1].SetActive(false);
					temp[column, row+2].SetActive(false);
				}
				else if(tempBeMove.tag == temp[tempColumn, row+1].tag && temp[tempColumn, row+2].tag == tempBeMove.tag) {
					tempBeMove.SetActive(false);
					temp[tempColumn, row+1].SetActive(false);
					temp[tempColumn, row+2].SetActive(false);
				}
			}
			if(column > 1) { 
				if(temp[column, row].tag == temp[column-1, row].tag && temp[column, row].tag == temp[column-2, row].tag) {
					temp[column, row].SetActive(false);
					temp[column-1, row].SetActive(false);
					temp[column-2, row].SetActive(false);
				}
				else if(tempBeMove.tag == temp[tempColumn-1, row].tag && tempBeMove.tag == temp[tempColumn-2, row].tag) {
					tempBeMove.SetActive(false);
					temp[tempColumn-1, row].SetActive(false);
					temp[tempColumn-2, row].SetActive(false);
				}
			}
			if(column < Clone.ballNum-2) {
				if(temp[column, row].tag == temp[column+1, row].tag && temp[column, row].tag == temp[column+2, row].tag) {
					temp[column, row].SetActive(false);
					temp[column+1, row].SetActive(false);
					temp[column+2, row].SetActive(false);
				}
				else if(tempBeMove.tag == temp[tempColumn+1, row].tag && tempBeMove.tag == temp[tempColumn+2, row].tag) {
					tempBeMove.SetActive(false);
					temp[tempColumn+1, row].SetActive(false);
					temp[tempColumn+2, row].SetActive(false);
				}
			}
		}
	}
}
