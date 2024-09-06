using UnityEngine;
using UnityEngine.SceneManagement;

public class P_scene : MonoBehaviour
{
    public Player player; 

    void Update()
    {
        if (player != null && player.IsDead())
        {
            SceneManager.LoadScene("GameOver"); 
        }
    }
}
