using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ATK : MonoBehaviour
{
    [SerializeField] private float atkAdd = 10f;
    [SerializeField] private Player player;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>(); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (player != null)
            {
                player.atk += atkAdd;
                Debug.Log("공격력 증가");
                Debug.Log($"Player_atk : {player.atk}");
                Destroy(gameObject);
            }
        }
    }
}
