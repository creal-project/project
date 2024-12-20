using UnityEngine;
using UnityEngine.UI;

public class P_Hpbar : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private float smoothTime = 0.1f;

    private Player player; 
    private float targetHp;
    private float currentVelocity; 

    void Start()
    {
        player = FindObjectOfType<Player>(); 

        if (player != null)
        {
            hpbar.maxValue = player.Hp_max;
            hpbar.value = player.hp; 
            targetHp = player.hp;
            player.OnHpChanged += UpdateHp;
        }
    }

    void Update()
    {
        hpbar.maxValue = player.Hp_max;
        UpdateHp();
        if (hpbar != null)
        {
            hpbar.value = Mathf.SmoothDamp(hpbar.value, targetHp, ref currentVelocity, smoothTime);
        }
    }

    private void UpdateHp()
    {
        if (player != null)
        {
            targetHp = player.hp; 
        }
    }
}
