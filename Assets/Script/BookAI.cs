﻿using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class BookAI : MonoBehaviour {

	// What to chase?
	public Transform target;

	// How many times each second we will update our path
	public float updateRate = 2f;

	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;

	//The AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathisEnded = false;

	//The max distance drom the AI to a waypoint for it to continue to the next
	public float nextWaypointDistance = 3;

	private int currentWaypoint = 0;

	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
		
			Debug.LogError ("No Player found? SHIT!");
			return;
		}

		//Start a new path to the target position, return the result to OnPathCompletemethod method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath () {
		if(target == null) {

			return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds ( 1f/updateRate);
		StartCoroutine (UpdatePath());
	
	}

	public void OnPathComplete(Path p) {
		Debug.Log ("We got a path. Did it have an error?" + p.error);
		if (!p.error){
			path = p;
			currentWaypoint = 0;
		
		}
			
	
	
	}

	void FixedUpdate () {
		if(target == null) {

			return;
		}


		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathisEnded) {
				return;
			}

			Debug.Log ("End of path reached");
			pathisEnded = true;
			return;
		}
		pathisEnded = false;

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}


	
	}

}
