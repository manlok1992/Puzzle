using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {
	static public GameObject[,] puzzles;
	static public readonly int ballNum = 8;
	public GameObject[] prefab;
	int[,] randomIndex;
	// Use this for initialization
	void Start () {
		puzzles = new GameObject[ballNum,ballNum];
		randomIndex = new int[ballNum,ballNum];
		for(int i = 0; i < ballNum; i++) {
			for(int j = 0; j < ballNum; j++) {	
				var ball = (GameObject)GameObject.Instantiate(prefab[randomBall(i,j)], new Vector3(-3+i, -3+j, 0), Quaternion.identity);
				puzzles[i,j] = (GameObject)ball;
				TouchEvent temp = (TouchEvent)puzzles[i,j].GetComponent("TouchEvent");
				temp.row = j;
				temp.column = i;
			}
		}
	}

	int randomBall(int i, int j) {
		randomIndex[i,j] = UnityEngine.Random.Range(0, prefab.Length);
		return randomIndex[i,j];
	}

	public void fallDown(int row, int column, int disappType) {

	}

	public IEnumerator reborn(GameObject temp, int i, int j) {
		yield return new WaitForSeconds(1.0f);
		if(temp == null) {
			print ("i = "+i+" j = "+j);
			var ball = (GameObject)GameObject.Instantiate(prefab[randomBall(i, j)], new Vector3(-3+i, -3+j, 0), Quaternion.identity);
			puzzles[i, j] = ball;
			TouchEvent tempE = (TouchEvent)puzzles[i,j].GetComponent("TouchEvent");
			tempE.row = j;
			tempE.column = i;
		}
		print ("reborn");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
