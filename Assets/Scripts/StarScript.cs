using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
