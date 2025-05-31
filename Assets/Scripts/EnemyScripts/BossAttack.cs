using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 1f;
    public int numberOfProjectiles = 8; 

    private float timeSinceLastFire;

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireRate)
        {
            FireProjectiles();
            timeSinceLastFire = 0;
        }
    }

    void FireProjectiles()
    {
        float angleStep = 360f / numberOfProjectiles;
        float startAngle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector2 projectileDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = tmpObj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = projectileDirection;

            Collider2D collider = tmpObj.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = true; 
            }
        }
    }
}
