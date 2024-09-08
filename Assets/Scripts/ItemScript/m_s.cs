using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_s : MonoBehaviour
{
    [SerializeField] float M_SAdd;
    GameObject Player;
    bool GetBuff;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player.GetComponent<Player>().movementSpeed += M_SAdd;
            Destroy(gameObject);
        }
    }
}
