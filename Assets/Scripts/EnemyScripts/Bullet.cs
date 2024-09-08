using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  
    public float lifeTime = 1.25f; 
    public int damage = 10;    

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage); 
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall")){
            Destroy(gameObject);
        };
    }
}
