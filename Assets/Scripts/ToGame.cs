using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGame : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("시작");
        SceneManager.LoadScene("Room");
    }
}
