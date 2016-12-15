using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraftaeroplanestandardassetscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 100);
        transform.rotation = Quaternion.Euler(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * Time.deltaTime * 100);
        Debug.DrawRay(transform.position, transform.forward * 10000, Color.red);


	}
}
