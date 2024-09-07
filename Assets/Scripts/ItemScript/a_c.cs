using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_c : MonoBehaviour
{
    [SerializeField] float a_c_decrease = 10f;
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player.GetComponent<Player>().a_c -= a_c_decrease;
            Destroy(gameObject);
        }
    }
}
