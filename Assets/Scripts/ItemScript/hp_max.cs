using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_max : MonoBehaviour
{
    [SerializeField] int HP_maxAdd=50;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player.GetComponent<Player>().Hp_max += HP_maxAdd;
            Debug.Log("최대 체력 증가");
            Destroy(gameObject);
        }
    }
}
