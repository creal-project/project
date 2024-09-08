using UnityEngine;
using UnityEngine.UI;

public class B_Hpbar : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private float smoothTime = 0.3f;

    private BossController boss; 
    private float targetHp;
    private float currentVelocity; 

    void Start()
    {
        boss = FindObjectOfType<BossController>(); 

        if (boss != null)
        {
            hpbar.maxValue = boss.maxHp;
            hpbar.value = boss.bossHp; 
            targetHp = boss.bossHp;
            boss.OnHpChanged += UpdateHp;
        }
    }

    void Update()
    {
        if (hpbar != null)
        {
            hpbar.value = Mathf.SmoothDamp(hpbar.value, targetHp, ref currentVelocity, smoothTime);
        }
    }

    private void UpdateHp()
    {
        if (boss != null)
        {
            targetHp = boss.bossHp; 
        }
    }
}
