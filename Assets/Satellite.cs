using UnityEngine;
using System.Collections.Generic;

public class Satellite : MonoBehaviour {

    List<GameObject> planets;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Grabbable");
        planets = new List<GameObject>(p);
        rb = GetComponent<Rigidbody>();
        Debug.Log(planets.Count);
        //rb.AddForce(Vector3.forward * 10f);
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (GameObject planet in planets)
        {
            float planetGravity = planet.transform.localScale.x;
            Vector3 directionToPlanet = planet.transform.position - transform.position;
            float gravity = planetGravity / Vector3.SqrMagnitude(directionToPlanet);
            //float gravity = planetGravity;

            directionToPlanet.Normalize();
            rb.AddForce(directionToPlanet * gravity * 0.2f);
        }
	}
}
