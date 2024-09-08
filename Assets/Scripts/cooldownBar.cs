using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    [SerializeField]
    private Slider cdbar;
    [SerializeField]
    private float smoothTime = 0.3f;

    private Player player;
    private float currentVelocity;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            cdbar.minValue = 0;
            cdbar.maxValue = 1; 
            cdbar.value = 1; 
            player.OnCdChanged += UpdateCd;
        }
    }

    void Update()
    {
        if (cdbar != null && player != null)
        {
            float cooldownRatio = Mathf.InverseLerp(0, player.attackCooldown, player.currentAttackCooldown);
            cdbar.value = Mathf.SmoothDamp(cdbar.value, cooldownRatio, ref currentVelocity, smoothTime);
        }
        if (player.currentAttackCooldown == 0)
        {
            cdbar.value = 1;
        }
    }

    private void UpdateCd()
    {
    }
}
