using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraphVisualizer : MonoBehaviour {
	private const int NumNodes = 10;
	private int[,] matrix;
    private string[] texts = new string[10] {
        "IRON MAN\n\nActually Tony Stark, but in a big iron suit.\n\nHas lazer cannons in his gloves and jetpack shoes.",
        "SUPER MAN\n\nDisguised as Clark Kent, a journalist.",
        "WOLVERINE\n\nA man named Logan with adamantium skeleton",
        "BATMAN\n\nBruce Wayne had his parents killed, so he is mad.",
        "KEVIN DURANT\n\nVery good basketball player.",
        "WONDER WOMAN\n\nDiana Prince is some kind of amazon warrior.",
        "THE JOKER\n\nActually not a hero, but people seem to think he cool.",
        "THE RIDDLER\n\nA villian. He makes jokes.",
        "THE FLASH\n\nBarry Gordon is a fast guy.",
        "THE HULK\n\nBruce Banner is a scientist that gets big and strong and green when he gets mad."
    };

    private GameObject[] nodes;

    public GameObject nodePrefab;
    public GameObject edgePrefab;

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
            nodes[i] = Instantiate(nodePrefab);
			nodes [i].transform.position = transform.position + new Vector3 (i, Random.value * NumNodes, Random.value * NumNodes) * 2 / NumNodes;
			nodes [i].GetComponent<Renderer> ().material.color = Color.cyan;
            nodes[i].GetComponentInChildren<Text>().text = texts[i];
		}

        for (int i = 0; i < NumNodes; i++)
        {
            for (int j = i + 1; j < NumNodes; j++)
            {
                if (matrix[i, j] == 1)
                {
                    EdgeResizer edge = Instantiate(edgePrefab).GetComponent<EdgeResizer>();
                    edge.n1 = nodes[i];
                    edge.n2 = nodes[j];
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
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
