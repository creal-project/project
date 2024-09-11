using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGame : MonoBehaviour
{
    public GameObject panel;
    public GameObject deadPanel;
    public GameObject player;
    public void Awake(){
        Time.timeScale = 0f;
        panel.SetActive(true);
        deadPanel.SetActive(false);
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
    public void Update(){
        if(player.GetComponent<Player>().hp<=0){
            deadPanel.SetActive(true);
        }
    }
    public void Quit(){
        Application.Quit();
    }
}
