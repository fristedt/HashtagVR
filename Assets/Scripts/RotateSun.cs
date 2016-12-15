using UnityEngine;
using System.Collections;

public class RotateSun : MonoBehaviour {


    public Vector3 rotation;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotation * Time.deltaTime);

        GetComponent<Rigidbody>().mass = 1000 * transform.localScale.x; 
    }
}
