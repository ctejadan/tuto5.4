using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class door : MonoBehaviour {

    public int LevelToLoad;
    private gameMaster gm;
    public GameObject doorSound;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = ("Presione [E]");
            if (Input.GetKeyDown("e"))
            {
                doorSound.SetActive(true);
                SceneManager.LoadScene("Etapa1");

            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene("Etapa1");
                doorSound.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = (" ");
        }
    }
}
