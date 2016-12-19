using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Blue_enemy : MonoBehaviour {

	public float velocity = 1f;
	public bool grounded;
	private Rigidbody2D rb2d;

	private bool _canJump = true;

	public Transform startPos;
	public Transform endPos;
	public LayerMask groundLayer;
	public bool colliding;

	public Transform instadeath;

	public int curHealt;
	public int MaxHealt =3;

    public GameObject explotionSound;

    public ParticleSystem deathEffect;


	private Player player;
	private Animator _animation;

	int i;
	Animator anim;
	// Use this for initialization
	void Start () {
		_animation = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		rb2d = gameObject.GetComponent<Rigidbody2D>(); 
		curHealt = MaxHealt;
	
	}
	
	// Update is called once per frame
	void Update () {
	/*	_animation.SetBool ("Jump", true);
		if (_canJump == true) {
			StartCoroutine ("EnemyMoveUP");
			new WaitForSeconds (50000);
		} 
			StartCoroutine ("EnemyMoveDOWN");
*/
		//Jump ();
		colliding = Physics2D.Linecast (startPos.position, endPos.position, groundLayer.value);
		if (colliding) {
			_animation.SetBool ("Jump", false);
			velocity *= -1;
			_canJump = true;
			Jump ();

			if (curHealt <= 0) {
                explotionSound.SetActive(false);
                explotionSound.SetActive(true);
                Instantiate (deathEffect.gameObject,transform.position,transform.rotation);
				Destroy (gameObject);
			
			}
		}
	

}
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") {

			player.Damage (1);

			StartCoroutine (player.Knockback (0.02f, 150, player.transform.position));
		}
	}

	IEnumerator EnemyMoveUP()
	{

			rb2d.velocity = new Vector2(rb2d.velocity.x,10);

		_canJump = false;
		yield return new WaitForSeconds (2);
	}

	IEnumerator EnemyMoveDOWN()
	{

		rb2d.velocity = new Vector2(rb2d.velocity.x,-10);

		_canJump = true;
		yield return new WaitForSeconds (2);
	}

	void Jump()
	{
		RaycastHit2D hit = Physics2D.Linecast (startPos.position, endPos.position, groundLayer.value);

		if (hit.collider != null) {
		
			_animation.SetBool ("Jump", false);
			_canJump = true;
		}
		if (_canJump == true) {
			_animation.SetBool ("Jump", true);
			rb2d.velocity = new Vector2 (rb2d.velocity.x, 10);
		}
		_canJump = false;

	}
	public void Damage(int damage)
	{
		curHealt -= damage;
		gameObject.GetComponent<Animation> ().Play ("Player(Redflash)");
	}


}

	
