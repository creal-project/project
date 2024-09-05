using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private void Start()
    {
        if(Random.value < 0.5f && transform.parent != null && !transform.parent.CompareTag("1"))
        {
            int enemyCount = Random.Range(2, 4);

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 randomPosition = new Vector3(
                    transform.position.x + Random.Range(-5.5f, 5.5f),
                    transform.position.y + Random.Range(-2.5f, 2.5f),
                    transform.position.z
                );
                //Instantiate(Enemy, randomPosition, transform.rotation, transform);
            }
        }
            
    }

}
