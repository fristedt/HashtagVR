using UnityEngine;
using System.Collections.Generic;

public class Satellite : MonoBehaviour {

    private Vector3 startPosition = new Vector3(-2.5f, 1.85f, 0);
    private Vector3 startRotation = new Vector3(16.954f, 90f, 0);

    List<GameObject> planets;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Grabbable");
        planets = new List<GameObject>(p);
        rb = GetComponent<Rigidbody>();
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(startRotation);
        rb.AddForce(transform.forward * 5);
    }
	
	// Update is called once per frame
	void Update () {
	    foreach (GameObject planet in planets)
        {
            float radius = planet.GetComponent<Rigidbody>().mass;
            float planetGravity = radius * 0.001f;
            Vector3 directionToPlanet = planet.transform.position - transform.position;
            float gravity = planetGravity / Vector3.SqrMagnitude(directionToPlanet);

            directionToPlanet.Normalize();
            rb.AddForce(directionToPlanet * gravity * 0.2f);
        }
	}
}
