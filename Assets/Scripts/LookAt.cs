using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    private GameObject target;

    private GameObject parent;
    private float scale = 0.1f;


	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(scale, scale, scale);
        parent = transform.parent.gameObject;
        target = GameObject.Find("Camera (eye)");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 position = parent.transform.position + direction * (parent.transform.localScale.x / 2);

        transform.position = position;
        transform.LookAt(target.transform.position);

    }
}
