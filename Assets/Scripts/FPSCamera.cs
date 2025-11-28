using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public Transform playerBody;      // プレイヤー本体
    public float mouseSensitivity = 150f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウス固定
    }

    void Update()
    {
        // マウス入力
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 上下回転（カメラだけ動かす）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // 上下制限
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 左右回転（プレイヤーの体を動かす)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
