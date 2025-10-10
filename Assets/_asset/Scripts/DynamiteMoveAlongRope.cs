using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteMoveAlongRope : MonoBehaviour
{
    private Transform startPoint;
    private Transform hookPoint;
    private float t = 0f;
    private bool moving = false;

    public void Setup(Transform start, Transform hook)
    {
        startPoint = start;
        hookPoint = hook;
        moving = true;
    }

    void Update()
    {
        if (!moving || startPoint == null || hookPoint == null) return;

        // Đi từ người ném đến hook theo đường thẳng
        t += Time.deltaTime * 1.5f; // tốc độ đi có thể chỉnh
        transform.position = Vector3.Lerp(startPoint.position, hookPoint.position, t);

        // Hướng quay theo dây
        Vector2 dir = hookPoint.position - startPoint.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (t >= 1f)
        {
            moving = false;
            // Ở đây có thể gọi nổ hoặc huỷ
            // Destroy(gameObject, 0.1f);
        }
    }
}
