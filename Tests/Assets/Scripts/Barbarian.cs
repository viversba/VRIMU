using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour {

	[Range(0f,0.5f)]
	public float speed;
	public int health;

	private bool isWalking;
	private Animator animator;
	private TextMesh healthText;
	private GameObject skeleton;
	private Quaternion initialRotation;
	private Vector3 distanceFromPlayerToBarbarian;

	// Use this for initialization
	void Start () {

		isWalking = true;

		healthText = transform.GetChild(2).GetComponent<TextMesh> ();
		animator = GetComponent<Animator> ();
		animator.SetTrigger ("Walk");
		animator.SetTrigger ("Mean");

		initialRotation = transform.rotation;
		skeleton = FindObjectOfType<IMU> ().gameObject;
		distanceFromPlayerToBarbarian = skeleton.transform.position - transform.position;
		distanceFromPlayerToBarbarian = distanceFromPlayerToBarbarian.normalized;
	}

	void Update () {

		transform.rotation = initialRotation;
		if(isWalking)
			transform.position = transform.position + distanceFromPlayerToBarbarian * speed;
		VerifyHealth ();
	}

	void VerifyHealth(){

		healthText.text = health.ToString ();
		if (health <= 0) {

			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){

		if (collision.gameObject.CompareTag("Player")) {
			isWalking = false;
			animator.SetTrigger ("RoundKick");
		}
	}

	public void CauseDamage(int damage){

		skeleton.GetComponent<IMU> ().InfringeDamage (damage);
	}

	public void InfringeDamage(int damage){

		health -= damage;
	}
}
