using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_max : MonoBehaviour
{
    [SerializeField] int HP_maxAdd=50;
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player.GetComponent<Player>().Hp_max += HP_maxAdd;
            Destroy(gameObject);
        }
    }
}
