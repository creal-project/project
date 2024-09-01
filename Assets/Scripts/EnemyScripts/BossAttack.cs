using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // �߻�ü ������
    public Transform firePoint;
    public float projectileSpeed = 10f; // �߻�ü �ӵ�
    public float fireRate = 1f; // �߻� �ӵ�
    public int numberOfProjectiles = 8; // �ѹ��� �߻��� �߻�ü ��

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
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirY = firePoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 projectileVector = new Vector3(projectileDirX, projectileDirY, 0f);
            Vector2 projectileMoveDirection = (projectileVector - firePoint.position).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
}
