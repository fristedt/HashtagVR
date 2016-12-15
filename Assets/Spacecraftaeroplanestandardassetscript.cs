using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraftaeroplanestandardassetscript : MonoBehaviour {
    Vector3 dir;

	// Use this for initialization
	void Start () {
        transform.LookAt(Vector3.zero);
        dir = -transform.position.normalized;
        Destroy(gameObject, 100);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(dir * Time.deltaTime * 100);
	}
}
