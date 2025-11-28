using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target;     // プレイヤー
    public float distance = 5f;  // 通常の距離
    public float smooth = 0.1f;  // なめらな補正
    public LayerMask wallMask;   // 壁レイヤー

    private float currentDistance;

    void Start()
    {
        currentDistance = distance;
    }

    void LateUpdate()
    {
        // プレイヤーからカメラの理想位置
        Vector3 desiredPos = target.position - target.forward * distance;

        // 衝突判定
        if (Physics.Raycast(target.position, -target.forward, out RaycastHit hit, distance, wallMask))
        {
            currentDistance = hit.distance - 0.2f; // 壁の手前に寄る
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, distance, smooth);
        }

        // カメラ位置更新
        transform.position = target.position - transform.forward * currentDistance;
    }
}
