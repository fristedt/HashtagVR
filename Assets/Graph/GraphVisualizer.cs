using UnityEngine;
using System.Collections;

public class GraphVisualizer : MonoBehaviour {
	private const int NumNodes = 10;
	private int[,] matrix;

	private GameObject[] nodes;

	// Use this for initialization
	void Start () {
		matrix = new int[NumNodes, NumNodes];
		for (int i = 0; i < NumNodes; i++) {
			for (int j = i + 1; j < NumNodes; j++) {

				matrix [i, j] = Random.Range (0, 2);
			}
		}

		nodes = new GameObject[NumNodes];
		for (int i = 0; i < NumNodes; i++) {
			nodes [i] = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			nodes [i].transform.position = new Vector3 (i, Random.value * NumNodes, Random.value * NumNodes);
			nodes [i].GetComponent<Renderer> ().material.color = Color.cyan;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < NumNodes; i++) {
			for (int j = i + 1; j < NumNodes; j++) {
				if (matrix [i, j] == 1) {
					Debug.DrawLine (nodes [i].transform.position, nodes [j].transform.position, Color.black);
				}
			}
		}
	}

	private void PrintMatrix() {
		for (int i = 0; i < NumNodes; i++) {
			string row = "";
			for (int j = 0; j < NumNodes; j++) {
				row += "" + matrix [i, j];
			}
			Debug.Log (row + "\n");
		}
	}

}
