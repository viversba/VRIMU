using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sowrd : MonoBehaviour {

	public int damage;

	private Quaternion initialRotation;
	private Transform parent, grandParent;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		initialRotation = transform.rotation;
		parent = transform.parent.transform;
		grandParent = transform.parent.transform.parent.transform;
		offset = transform.position - parent.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){


		if (collision.gameObject.GetComponent<Barbarian> ()) {

			print ("IMPAKTADO");	
			collision.gameObject.GetComponent<Barbarian> ().InfringeDamage (damage);
		}
	}
}
