using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    //arreglo con los corazones
    public Sprite[] HeartSprites;

    //imagen de los corazones
    public Image HeartUI;

    //player
    private Player player;

    void Start ()
    {
        //player=Player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update ()
    {
        //se van restando corazones a medida que pierde vida
        HeartUI.sprite = HeartSprites[player.curHealth];
    }
}
