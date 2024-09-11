using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGame : MonoBehaviour
{
    public GameObject panel;
    public void Awake(){
        Time.timeScale = 0f;
        panel.SetActive(true);
    }
    public void OnClick()
    {
        Debug.Log("시작");
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
