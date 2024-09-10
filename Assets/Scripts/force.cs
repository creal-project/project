using UnityEngine;
 
public class force : MonoBehaviour
{
    public float knockbackForce = 5f; // 넉백의 강도를 조절할 변수
    public float knockbackDuration = 0.2f; // 넉백의 지속 시간

    private bool isKnockedBack = false; // 넉백 상태를 추적하는 변수
    private Vector3 knockbackDirection; // 넉백 방향을 저장할 변수
    private float knockbackEndTime; // 넉백이 끝나는 시간을 저장할 변수

    private GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // 넉백 상태일 때, 넉백 효과를 적용합니다.
        if (isKnockedBack)
        {
            // 넉백이 끝난 시간까지 남은 시간 동안 넉백 효과를 적용합니다.
            if (Time.time < knockbackEndTime)
            {
                // 넉백 방향으로 캐릭터를 이동시킵니다.
                transform.position += knockbackDirection * knockbackForce * Time.deltaTime;
            }
            else
            {
                // 넉백이 끝났으므로 넉백 상태를 해제합니다.
                isKnockedBack = false;
                player.GetComponent<Player>().isHit = false;
            }
        }
    }

    public void ApplyKnockback(Vector3 direction)
    {
        // 넉백 방향을 저장합니다.
        knockbackDirection = direction.normalized;
        // 넉백 상태를 활성화합니다.
        isKnockedBack = true;
        // 넉백이 끝나는 시간을 설정합니다.
        knockbackEndTime = Time.time + knockbackDuration;
    }
}