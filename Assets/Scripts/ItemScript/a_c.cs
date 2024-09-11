using UnityEngine;

public class a_c : MonoBehaviour
{
    [SerializeField] float attackCooldown_decrease = 1f;
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player.GetComponent<Player>().attackCooldown -= attackCooldown_decrease;
            Destroy(gameObject);
        }
    }
}
