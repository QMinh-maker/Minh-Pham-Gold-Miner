using UnityEngine;

public class ThrowingDynamite : MonoBehaviour
{
    public GameObject DynamitePrefab;  // Prefab dynamite
    public Transform hookTarget;       // Móc kéo (hook) – gán trong Inspector
    public Vector3 DynamiteOffset;     // Vị trí lệch khi ném
    public float moveSpeed = 5f;       // Tốc độ bay xuống

    public void ShootOnce()
    {
        if (hookTarget == null)
        {
            Debug.LogWarning("Chưa gán hookTarget cho ThrowingDynamite!");
            return;
        }

        // Tạo viên dynamite tại vị trí người ném
        GameObject dynamite = Instantiate(DynamitePrefab, transform.position + DynamiteOffset, Quaternion.identity);

        // Thêm script điều khiển bay xuống
        var moveScript = dynamite.AddComponent<DynamiteMoveAlongRope>();
        moveScript.Setup(transform, hookTarget);
    }
}

