using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public float velocity;
	private Rigidbody2D rb2d;

	public float move;
	private float initposition;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>(); 
		initposition = transform.position.y;

	}

	// Update is called once per frame
	void Update () {

		rb2d.AddForce(Vector2.up * velocity);

		if(transform.position.y > initposition+ move ) {
			//fuerza del salto

			rb2d.AddForce(Vector2.up * -velocity);
		}
	}

}
