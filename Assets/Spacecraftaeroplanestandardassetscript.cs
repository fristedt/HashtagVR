using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraftaeroplanestandardassetscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 100);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * 100);


	}
}
