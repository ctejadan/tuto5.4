using UnityEngine;
using System.Collections;

public class CloudX : MonoBehaviour {
	public float velocity;
	private Rigidbody2D rb2d;

	public float move;
	private float initposition;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>(); 
		initposition = transform.position.x;

	}

	// Update is called once per frame
	void Update () {

		rb2d.AddForce(Vector2.left * velocity);

		if(transform.position.x < initposition+ 30f ) {

			rb2d.AddForce(Vector2.left * -velocity);
		}
}
}
