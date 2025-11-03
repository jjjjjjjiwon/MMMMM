using UnityEngine;

public class FollowCamera : MonoBehaviour
{
   public Transform player;           // 플레이어 Transform
    public Vector3 offset = new Vector3(0, 2, -4);
    public float mouseSensitivity = 2f;
    public float rotationSmooth = 5f;

    private float pitch = 0f;
    private float yaw = 0f;

    void Awake()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -35f, 60f);

        Quaternion camRot = Quaternion.Euler(pitch, yaw, 0);
        transform.position = player.position + camRot * offset;
        transform.rotation = camRot;
    }
}

