using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    [SerializeField] int heal_HP=20;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<Player>().hp >= Player.GetComponent<Player>().Hp_max){
            Player.GetComponent<Player>().hp = Player.GetComponent<Player>().Hp_max;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Player.GetComponent<Player>().hp <= Player.GetComponent<Player>().Hp_max)
            {
                Player.GetComponent<Player>().hp += heal_HP;
                Destroy(gameObject);
            }
        }
    }
}
