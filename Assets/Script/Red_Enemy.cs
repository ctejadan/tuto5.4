using UnityEngine;
using System.Collections;

public class Red_Enemy : MonoBehaviour {

	public float velocity = 100f;

	public Transform sightStart;
	public Transform sightEnd;

	public ParticleSystem deathEffect;
    public LayerMask detectWhat;
	 
	public bool colliding;

	public Transform weakness;

	private Player player;

	private Animator anim;

	public int curHealth;
	public int MaxHealth =3;




	//Animator anim;


	// Use this for initialization
	void Start () {
	
		anim = gameObject.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        curHealth = MaxHealth;

	}
	
	// Update is called once per frame
	void Update ()
	{
		//anim.SetBool("Stomped",false);

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-velocity, GetComponent<Rigidbody2D> ().velocity.y); 

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectWhat);

		if (colliding) {

			transform.localScale = new Vector2 (transform.localScale.x * -1 , transform.localScale.y);
			velocity *= -1;
		}
		if (curHealth <= 0) {
			Vector3 position = transform.position;
			Instantiate (deathEffect.gameObject,transform.position,transform.rotation);

			Destroy (gameObject);

		}
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {

			float height = col.contacts [0].point.y - weakness.position.y;

			if (height > 0) {
			//	gameObject.GetComponent<Animation> ().Play ("Enemy_Red_Death");

				Dies ();
			
			} else {
				player.Damage (1);

				StartCoroutine (player.Knockback ( 0.02f, 150, player.transform.position));
			}
		}
	}

	void Dies()
	{

   
            transform.localScale = new Vector2(transform.localScale.x * 0, transform.localScale.y);
            anim.SetBool("Stomped", true);

            Destroy(this.gameObject, 0.01f);
          

        
	}



	public void Damage(int damage)
	{
		curHealth -= damage;

	}

}
