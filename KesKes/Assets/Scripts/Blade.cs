using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {
	public GameObject bladeTrailPrefab;
	bool isCutting = false;
	public float minCuttingVelocity = .001f;

	Vector2 previousPosition;

	Rigidbody2D rb;
	Camera cam;
	CircleCollider2D coll;

	GameObject currentBladeTrail;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		rb = GetComponent<Rigidbody2D> ();
		coll = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) 
		{
			StartCutting ();
		}else if(Input.GetMouseButtonUp(0))
		{
			StopCutting ();
		}

		if (isCutting) {
			UpdateCut ();
		}
	}

	void UpdateCut()
	{
		Vector2 newPosition  = cam.ScreenToWorldPoint(Input.mousePosition);
		rb.position = newPosition;

		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
		if (velocity > minCuttingVelocity) {
			coll.enabled = true;
		} else {
			coll.enabled = false;
		}
		previousPosition = newPosition;
	}

	void StartCutting()
	{
		isCutting = true;
		currentBladeTrail = Instantiate (bladeTrailPrefab, transform);	
		previousPosition = cam.ScreenToWorldPoint (Input.mousePosition);
		coll.enabled = false;
	}

	void StopCutting()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent (null);
		Destroy (currentBladeTrail, 2f);
		coll.enabled = false;
	}
}
