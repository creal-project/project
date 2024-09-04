using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_c : MonoBehaviour
{
    [SerializeField] float a_c_decrease = 10f;
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
            Player.GetComponent<Player>().a_c -= a_c_decrease;
            Debug.Log("공격속도 증가");
            Destroy(gameObject);
        }
    }
}
