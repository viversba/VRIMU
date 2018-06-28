using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianSpawn : MonoBehaviour {

	public float timeDelay;
	public float startTime;

	public GameObject barbarianPrefab;

	private bool instantiated;
	private float start;

	// Use this for initialization
	void Start () {

		instantiated = false;
		start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - start > startTime) {

			if (transform.childCount < 2 && !instantiated) {

				if (timeDelay > 5f) {
					timeDelay -= -1f;
				}
				instantiated = true;
				GameObject barbarian = Instantiate (barbarianPrefab);
				barbarian.transform.rotation = transform.rotation;
				barbarian.transform.position = transform.position;
				barbarian.transform.SetParent (gameObject.transform);
				StartCoroutine(SpawnDelay());
			}
		}
	}

	IEnumerator SpawnDelay(){
	
		yield return new WaitForSeconds(timeDelay);
		instantiated = false;
	}
}
