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
        GetBuff = false;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetBuff = true;
        }
    }

    public void Buff()
    {
        if (GetBuff == true)
        {
            Player.GetComponent<Player>().m_s += M_SAdd;
            Debug.Log("이동 속도 증가 증가");
            Destroy(gameObject);
        }
    }
}
