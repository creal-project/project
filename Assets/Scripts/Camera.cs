using UnityEngine;

public class Camera : MonoBehaviour
{
    public float LerpSmooth = 0.125f;
    public void moveCamera()
    {
        if(RoomManager.Instance.CurrentRoom == null)
        {
            return;
        }
        Vector3 Target = RoomManager.Instance.CurrentRoom.transform.position;
        Target.z = -10f;
        transform.position = Vector3.Lerp(transform.position, Target, LerpSmooth*Time.deltaTime);
    }
    private void FixedUpdate()
    {
        moveCamera();
    }
}
