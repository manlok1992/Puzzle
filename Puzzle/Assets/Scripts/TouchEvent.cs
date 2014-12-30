using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {
	public bool isTouchDown = false;
	enum Direction{LEFT, RIGHT, UP, DOWN, NONE};
	Direction dir;
	public int column, row;
	public bool isMove = false;
	static int destroyCount = 0;
	public float disapearTimer = 0;
	// Use this for initialization
	void Start () {
		dir = Direction.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		disapearTimer += Time.deltaTime;
		if(disapearTimer >= 0.5f)
			Match(Direction.NONE);
		#if UNITY_IPHONE || UNITY_ANDROID
		if(isTouchDown) {
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				if(touchDeltaPosition.x < -10) {
					//LEFT
					dir = Direction.LEFT;
					if(!isMove)
						moveLeft();
				}
				if(touchDeltaPosition.x > 10) {
					//RIGHT
					dir = Direction.RIGHT;
					if(!isMove)
						moveRight();
				}
				if(touchDeltaPosition.y < -10) {
					//DOWN
					dir = Direction.DOWN;
					if(!isMove)
						moveDown();
				}
				if(touchDeltaPosition.y > 10) {
					//UP
					dir = Direction.UP;
					if(!isMove)
						moveUp();
				}
				if(touchDeltaPosition.x < 10 && touchDeltaPosition.x > -10 && touchDeltaPosition.y > -10 && touchDeltaPosition.y < 10) {
					dir = Direction.NONE;
				}
			}
		}
#endif
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX
		if(isTouchDown) {
			if(Input.GetAxis("Mouse X") < -0.1) {
				//Left
				dir = Direction.LEFT;
				if(!isMove)
					moveLeft();
			}
			if(Input.GetAxis("Mouse X") > 0.1) {
				//Right
				dir = Direction.RIGHT;
				if(!isMove)
					moveRight();
			}
			if(Input.GetAxis("Mouse Y") < -0.1) {
				//Down
				dir = Direction.DOWN;
				if(!isMove)
					moveDown();
			}
			if(Input.GetAxis("Mouse Y") > 0.1) {
				//Up
				dir = Direction.UP;
				if(!isMove)
					moveUp();
			}
			if(Input.GetAxis("Mouse X") < 0.1 && Input.GetAxis("Mouse X") > -0.1 && 
			   Input.GetAxis("Mouse Y") < 0.1 && Input.GetAxis("Mouse Y") > -0.1) {
				dir = Direction.NONE;
			}
		}
		
		#endif
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

	void removeBall(GameObject temp1, GameObject temp2, GameObject temp3) {
		Destroy(temp1);
		Destroy(temp2);
		Destroy(temp3);
		for(int i = 0; i < Clone.ballNum; i++) {
			for(int j = 0; j < Clone.ballNum; j++) {
				var tempObj = Clone.puzzles[i, j];
				if(tempObj == temp1) {
					Clone.puzzles[i, j] = null;
					print ("Remove "+i+" "+j);
				}
				else if(tempObj == temp2) {
					Clone.puzzles[i, j] = null;
					print ("Remove "+i+" "+j);
				}
				else if(tempObj == temp3) {
					Clone.puzzles[i, j] = null;
					print ("Remove "+i+" "+j);
				}
//				print ("tempObj = "+Clone.puzzles[i, j].name);
			}
		}
	}

	void Match(Direction tempDir) {
		GameObject[,] temp = Clone.puzzles;
//		if(tempDir == Direction.DOWN || tempDir == Direction.UP) {
		GameObject tempMove = temp[column, row];
		GameObject tempBeMove = null;
		int tempRow = 0;
		if(tempDir == Direction.DOWN || tempDir == Direction.NONE) {
			if(tempDir != Direction.NONE)
				tempBeMove = temp[column, row+1];
			tempRow = row+1;
		}
		else if(tempDir == Direction.UP || tempDir == Direction.NONE) {
			if(tempDir != Direction.NONE)
				tempBeMove = temp[column, row-1];
				tempRow = row-1;
		}

		if(temp[column, row] && column != 0 && column != Clone.ballNum-1) {
			if(temp[column, row].tag == temp[column-1, row].tag && temp[column+1, row].tag == temp[column, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column-1, row].SetActive(false);
//				temp[column+1, row].SetActive(false);
				removeBall(temp[column, row], temp[column-1, row], temp[column+1, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-1, row], column-1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+1, row], column+1, row));
//				cloneObj.fallDown(column-1, row, 1);
			}
			else if(tempBeMove != null && tempBeMove.tag == temp[column-1, tempRow].tag && temp[column+1, tempRow].tag == tempBeMove.tag) {
//				tempBeMove.SetActive(false);
//				temp[column-1, tempRow].SetActive(false);
//				temp[column+1, tempRow].SetActive(false);
				removeBall(tempBeMove, temp[column-1, tempRow], temp[column+1, tempRow]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow], column, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-1, tempRow], column-1, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+1, tempRow], column+1, tempRow));
			}
		}
		if(temp[column, row] && column > 1) {
			if(temp[column, row].tag == temp[column-1, row].tag && temp[column-2, row].tag == temp[column, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column-1, row].SetActive(false);
//				temp[column-2, row].SetActive(false);
				removeBall(temp[column, row], temp[column-1, row], temp[column-2, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-1, row], column-1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-2, row], column-2, row));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[column-1, tempRow].tag && temp[column-2, tempRow].tag == tempBeMove.tag) {
//				tempBeMove.SetActive(false);
//				temp[column-1, tempRow].SetActive(false);
//				temp[column-2, tempRow].SetActive(false);
				removeBall(tempBeMove, temp[column-1, tempRow], temp[column-2, tempRow]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow], column, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-1, tempRow], column-1, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-2, tempRow], column-2, tempRow));
			}
		}
		if(temp[column, row] && column < Clone.ballNum-2) {
			if(temp[column, row].tag == temp[column+1, row].tag && temp[column+2, row].tag == temp[column, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column+1, row].SetActive(false);
//				temp[column+2, row].SetActive(false);
				removeBall(temp[column, row], temp[column+1, row], temp[column+2, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+1, row], column+1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+2, row], column+2, row));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[column+1, tempRow].tag && temp[column+2, tempRow].tag == tempBeMove.tag) {
//				tempBeMove.SetActive(false);
//				temp[column+1, tempRow].SetActive(false);
//				temp[column+2, tempRow].SetActive(false);
				removeBall(tempBeMove, temp[column+1, tempRow], temp[column+2, tempRow]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow], column, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+1, tempRow], column+1, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+2, tempRow], column+2, tempRow));
			}
		}
		
		if(temp[column, row] && row > 1) { 
			if(temp[column, row].tag == temp[column, row-1].tag && temp[column, row].tag == temp[column, row-2].tag) {
//				temp[column, row].SetActive(false);
//				temp[column, row-1].SetActive(false);
//				temp[column, row-2].SetActive(false);
				removeBall(temp[column, row], temp[column, row-1], temp[column, row-2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row-1], column, row-1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row-2], column, row-2));
				cloneObj.fallDown(row, column, 0);
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[column, tempRow-1].tag && tempBeMove.tag == temp[column, tempRow-2].tag) {
//				tempBeMove.SetActive(false);
//				temp[column, tempRow-1].SetActive(false);
//				temp[column, tempRow-2].SetActive(false);
				removeBall(tempBeMove, temp[column, tempRow-1], temp[column, tempRow-2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow], column, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow-1], column, tempRow-1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow-2], column, tempRow-2));
			}
		}
		if(temp[column, row] && row < Clone.ballNum-2) {
			if(temp[column, row].tag == temp[column, row+1].tag && temp[column, row].tag == temp[column, row+2].tag) {
//				temp[column, row].SetActive(false);
//				temp[column, row+1].SetActive(false);
//				temp[column, row+2].SetActive(false);
				removeBall(temp[column, row], temp[column, row+1], temp[column, row+2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row+1], column, row+1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row+2], column, row+2));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[column, tempRow+1].tag && tempBeMove.tag == temp[column, tempRow+2].tag) {
//				tempBeMove.SetActive(false);
//				temp[column, tempRow+1].SetActive(false);
//				temp[column, tempRow+2].SetActive(false);
				removeBall(tempBeMove, temp[column, tempRow+1], temp[column, tempRow+2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow], column, tempRow));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow+1], column, tempRow+1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, tempRow+2], column, tempRow+2));
			}
		}
//		}
//		if(tempDir == Direction.LEFT || tempDir == Direction.RIGHT) {
//			GameObject tempMove = temp[column, row];
//			GameObject tempBeMove = null;
			int tempColumn = 0;
		if(tempDir == Direction.LEFT || tempDir == Direction.NONE) {
			if(tempDir != Direction.NONE)
				tempBeMove = temp[column+1, row];
				tempColumn = column+1;
			}
		else if(tempDir == Direction.RIGHT || tempDir == Direction.NONE) {
			if(tempDir != Direction.NONE)
				tempBeMove = temp[column-1, row];
				tempColumn = column-1;
			}
		if(temp[column, row] && row != 0 && row != Clone.ballNum-1) {
			if(temp[column, row].tag == temp[column, row-1].tag && temp[column, row+1].tag == temp[column, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column, row-1].SetActive(false);
//				temp[column, row+1].SetActive(false);
				removeBall(temp[column, row], temp[column, row-1], temp[column, row+1]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
                cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row-1], column, row-1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row+1], column, row+1));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[tempColumn, row-1].tag && temp[tempColumn, row+1].tag == tempBeMove.tag) {
//				tempBeMove.SetActive(false);
//				temp[tempColumn, row-1].SetActive(false);
//				temp[tempColumn, row+1].SetActive(false);
				removeBall(tempBeMove, temp[tempColumn, row-1], temp[tempColumn, row+1]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row], tempColumn, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row-1], tempColumn, row-1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row+1], tempColumn, row+1));
			}
		}
		if(temp[column, row] && row > 1) {
			if(temp[column, row].tag == temp[column, row-1].tag && temp[column, row-2].tag == temp[column, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column, row-1].SetActive(false);
//				temp[column, row-2].SetActive(false);
				removeBall(temp[column, row], temp[column, row-1], temp[column, row-2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row-1], column, row-1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row-2], column, row-2));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[tempColumn, row-1].tag && temp[tempColumn, row-2].tag == tempBeMove.tag) {
//				tempBeMove.SetActive(false);
//				temp[tempColumn, row-1].SetActive(false);
//				temp[tempColumn, row-2].SetActive(false);
				removeBall(tempBeMove, temp[tempColumn, row-1], temp[tempColumn, row-2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row], tempColumn, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row-1], tempColumn, row-1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row-2], tempColumn, row-2));
			}
		}
		if(temp[column, row] && row < Clone.ballNum-2) {
			if(temp[column, row].tag == temp[column, row+1].tag && temp[column, row+2].tag == temp[column, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column, row+1].SetActive(false);
//				temp[column, row+2].SetActive(false);
				removeBall(temp[column, row], temp[column, row+1], temp[column, row+2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row+1], column, row+1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row+2], column, row+2));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[tempColumn, row+1].tag && temp[tempColumn, row+2].tag == tempBeMove.tag) {
//				tempBeMove.SetActive(false);
//				temp[tempColumn, row+1].SetActive(false);
//				temp[tempColumn, row+2].SetActive(false);
				removeBall(tempBeMove, temp[tempColumn, row+1], temp[tempColumn, row+2]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row], tempColumn, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row+1], tempColumn, row+1));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row+2], tempColumn, row+2));
			}
		}
		if(temp[column, row] && column > 1) { 
			if(temp[column, row].tag == temp[column-1, row].tag && temp[column, row].tag == temp[column-2, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column-1, row].SetActive(false);
//				temp[column-2, row].SetActive(false);
				removeBall(temp[column, row], temp[column-1, row], temp[column-2, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-1, row], column-1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column-2, row], column-2, row));
			}
			else if(tempBeMove != null && tempBeMove.tag == temp[tempColumn-1, row].tag && tempBeMove.tag == temp[tempColumn-2, row].tag) {
//				tempBeMove.SetActive(false);
//				temp[tempColumn-1, row].SetActive(false);
//				temp[tempColumn-2, row].SetActive(false);
				removeBall(tempBeMove, temp[tempColumn-1, row], temp[tempColumn-2, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row], tempColumn, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn-1, row], tempColumn-1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn-2, row], tempColumn-2, row));
			}
		}
		if(temp[column, row] && column < Clone.ballNum-2) {
			if(temp[column, row].tag == temp[column+1, row].tag && temp[column, row].tag == temp[column+2, row].tag) {
//				temp[column, row].SetActive(false);
//				temp[column+1, row].SetActive(false);
//				temp[column+2, row].SetActive(false);
				removeBall(temp[column, row], temp[column+1, row], temp[column+2, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column, row], column, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+1, row], column+1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[column+2, row], column+2, row));
			}
		else if(tempBeMove != null && tempBeMove.tag == temp[tempColumn+1, row].tag && tempBeMove.tag == temp[tempColumn+2, row].tag) {
//				tempBeMove.SetActive(false);
//				temp[tempColumn+1, row].SetActive(false);
//				temp[tempColumn+2, row].SetActive(false);
				removeBall(tempBeMove, temp[tempColumn+1, row], temp[tempColumn+2, row]);
				Clone cloneObj = (Clone)GameObject.Find("Clone").GetComponent("Clone");
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn, row], tempColumn, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn+1, row], tempColumn+1, row));
				cloneObj.StartCoroutine(cloneObj.reborn(temp[tempColumn+2, row], tempColumn+2, row));
			}
		}
//		}
	}
}
