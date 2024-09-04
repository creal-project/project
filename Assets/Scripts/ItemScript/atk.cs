using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ATK : MonoBehaviour
{
    //
    [SerializeField] float AtkAdd = 10f;
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }
   void OnCollisionEnter2D(Collision2D collision)
   {
        if(collision.collider.CompareTag("Player"))
        {                
            Player.GetComponent<Player>().atk +=AtkAdd;
            Debug.Log("공격력 증가");
            Destroy(gameObject);
        }
   }
}
