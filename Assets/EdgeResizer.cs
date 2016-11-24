using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EdgeResizer : MonoBehaviour {
    private const float Width = 0.01f;

    public GameObject n1, n2;
    public GameObject plane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 start = n1.transform.position;
        Vector3 end = n2.transform.position;
        Vector3 offset = n1.transform.position - n2.transform.position;
        Vector3 scale = new Vector3(Width, offset.magnitude / 2.0f, Width);
        Vector3 position = start - (offset / 2.0f);

        transform.position = position;
        transform.up = offset;
        transform.localScale = scale;
    }
}
