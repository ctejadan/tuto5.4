using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Blue_enemy : MonoBehaviour {

	public float velocity = 1f;
	public bool grounded;
	private Rigidbody2D rb2d;

	public Transform instadeath;

	public float initposition;

	private Player player;


	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		rb2d = gameObject.GetComponent<Rigidbody2D>(); 
		initposition = transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		grounded = true;
		rb2d.AddForce(Vector2.up * velocity);

		if(transform.position.y > initposition+1f ) {
			//fuerza del salto
			rb2d.AddForce(Vector2.up * -velocity);
			grounded = false;
	}
}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {

					player.Damage (1);

					StartCoroutine (player.Knockback (0.02f, 150, player.transform.position));
				}
			}
		}
	
