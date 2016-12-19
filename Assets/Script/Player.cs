using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 150f;
//	public float knockPwrX = -100;

	public bool grounded;
	public bool canDoubleJump;

	public int curHealth;
	public int maxHealth = 5;


	private Rigidbody2D rb2d;
	private Animator anim;
    private gameMaster gm;

	private BookAI book;
	private Player player; 
	private GameObject enemy;

    public GameObject canvasDead;
    public GameObject heartSound;
    public GameObject baseSound;
    public GameObject painSound;

    AudioSource audio;



    // inicialización
    void Start () {

        //inicializa el objeto rigidbody2d
        rb2d = gameObject.GetComponent<Rigidbody2D>(); 
        //inicializa las animaciones
		anim = gameObject.GetComponent<Animator>();
        //inicializa el libro

		enemy = GameObject.FindGameObjectWithTag("Enemy");


        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();


        //SceneManager.LoadScene("scene4") <- selecciona escena
        curHealth = maxHealth;
        

        if (SceneManager.GetActiveScene().name != "Inicial")
        {
            book = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BookAI>();
        }


        //la vida del pj es igual a la vida máxima

		

    }

	// Se llama una vez por frame
	void Update () {
        //animación para cuando está en el piso
		anim.SetBool("Grounded",grounded);
        //velocidad de movimiento
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        //si se da vuelta en el eje x, se da vuelta el pj
		if (Input.GetAxis("Horizontal") < -0.1f)
		{
			transform.localScale = new Vector3(-1.5f, 1.5f, 1);
		}
        //si salta, se setea el porte del pj
		if (Input.GetAxis("Horizontal") > 0.1f)
		{
			transform.localScale = new Vector3(1.5f, 1.5f, 1);
		}
        //si se apreta saltar
		if(Input.GetButtonDown("Jump"))
		{
            //si esta en el piso
			if(grounded) {
                //fuerza del salto
				rb2d.AddForce(Vector2.up * jumpPower);
                //puede saltar 2 veces
				canDoubleJump = true;
			}
			else
			{
                //si puede saltar 2 veces
				if(canDoubleJump)
				{
                    //no puede volver a saltar 2 veces
					canDoubleJump = false;
					rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    //el segundo salto tiene menos fuerza que el primero
					rb2d.AddForce(Vector2.up * jumpPower / 1.3f);
				}
			}
		}
        //si la vida del pj es mayor a la vida máxima
		if (curHealth > maxHealth)
		{
            //la vida del personaje es igual a la vida máxima
			curHealth = maxHealth;
		}
        //si la vida del personaje es menor o igual a 0
		if(curHealth <= 0)
		{
            baseSound.SetActive(false);
            painSound.SetActive(true);
            //DIEEEE

            //audio.enabled = true;
            Die();
		}
	}

	void FixedUpdate(){
        //velocidad más "smooth"
		Vector3 easeVelocity = rb2d.velocity;
        //velocidad de y es igual a la velcoidad del rigid body 2 en y
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f; 

        //h = direccion horizontal
		float h = Input.GetAxis ("Horizontal");

		if(grounded)
		{
            //si esta en el suelo, la velocidad del rigid body es igual a la velocidad "smooth"
			rb2d.velocity = easeVelocity;
		}

        //le agrega velocidad al correr en el eje x
		rb2d.AddForce ((Vector2.right * speed) * h);

		//si la velocidad del rigid body es mayor a la velocidad máxima en el eje x
		if (rb2d.velocity.x > maxSpeed)
		{
            //la velocidad en el eje y es igual a la velocidad máxima
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
		}
            //lo mismo, pero hacia la izquierda (eje x negativo)
		if(rb2d.velocity.x < -maxSpeed)
		{
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
		}
	}	


	void Die()
	{
        //Se vuelve a iniciar el juego
        //Application.loadedLevel(Application.loadedLevel);//restart

        

        //audioSource.clip = death;
        //audioSource.Play();
        curHealth = 0;
        Time.timeScale = 0;
        heartSound.SetActive(true);

        canvasDead.SetActive(true);


        if (Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene("Inicial");

        }
        

	}

	public void Damage(int dmg)
	{
        //Se le resta el daño a la velocidad del pj
		curHealth-=dmg;
        //se muestra que el pj recibió daño
		gameObject.GetComponent<Animation> ().Play ("Player(Redflash)");

	}

	void OnTriggerEnter2D(Collider2D col)
	{


		if (col.CompareTag( "Spike")) 
		{
			Damage (5);

		}
	}

	public IEnumerator Knockback (float knockDur, float knockbackPwr, Vector3 knockbackDir )
	{
		float timer = 0;

		while (knockDur > timer)
		{
			timer += Time.deltaTime;


			//rb2d.velocity = new Vector2 (0, 0);
			if (enemy != null) {
				if (enemy.transform.position.x > transform.position.x) {

					rb2d.AddForce (new Vector3 (-200, knockbackPwr, transform.position.z));

					/*if (book.transform.position.y  >  transform.position.y) {
					rb2d.AddForce (new Vector3 (-200, -knockbackPwr, transform.position.z));
				}

				else {
					rb2d.AddForce (new Vector3 (-200, knockbackPwr, transform.position.z));
				}*/
				} else if (enemy.transform.position.x < transform.position.x) {

					rb2d.AddForce (new Vector3 (200, knockbackPwr, transform.position.z));


					/*if (book.transform.position.y > transform.position.y) {
					rb2d.AddForce (new Vector3 (200, -knockbackPwr, transform.position.z));
				} 

				else {
					rb2d.AddForce (new Vector3 (200, knockbackPwr, transform.position.z));
				}*/

				} else {
					rb2d.AddForce (new Vector3 (0, knockbackPwr, transform.position.z));

				}
			}
			else {
				yield return 0;
			}

		}

		yield return 0;
	}


}