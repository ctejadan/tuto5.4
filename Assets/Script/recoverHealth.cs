using UnityEngine;
using System.Collections;

public class recoverHealth : MonoBehaviour {

    public GameObject heart;
    public GameObject heartSound;
    private Player player;
    

	// Use this for initialization
	void Start () {
   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col) {

        if(col.CompareTag("Player"))
        {
            heartSound.SetActive(false);
            player.curHealth = player.curHealth + 1;
            heartSound.SetActive(true);
            heart.SetActive(false);
        }
	
	}
}
