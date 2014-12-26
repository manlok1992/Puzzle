using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {
	GameObject[,] puzzles;
	static readonly int ballNum = 4;
	public GameObject[] prefab;
	int[,] randomIndex;
	// Use this for initialization
	void Start () {
		puzzles = new GameObject[ballNum,ballNum];
		for(int i = 0; i < ballNum; i++) {
			for(int j = 0; j < ballNum; j++) {	
				var ball = GameObject.Instantiate(prefab[randomBall(i,j)], new Vector3(-3+i, -3+j, 0), Quaternion.identity);
				puzzles[i,j] = (GameObject)ball;
			}
		}
	}

	int randomBall(int i, int j) {
		randomIndex = new int[ballNum,ballNum];
		randomIndex[i,j] = UnityEngine.Random.Range(0, prefab.Length);
		return randomIndex[i,j];
	}

	// Update is called once per frame
	void Update () {
	
	}
}
