using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Transform startPosition;

    private float Line_width = 5f;

    private Vector3 fixedStartPos; // ✅ Ghi nhớ vị trí cố định của điểm neo

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = Line_width;
        lineRenderer.enabled = false;

        // Ghi lại vị trí ban đầu của startPosition (neo dây cố định)
        if (startPosition != null)
        {
            fixedStartPos = startPosition.position;
        }
        else
        {
            Debug.LogWarning("⚠️ RopeRenderer: Chưa gán startPosition!");
        }

        // Đảm bảo LineRenderer hoạt động ở tọa độ thế giới
        lineRenderer.useWorldSpace = true;
    }

    public void RenderLine(Vector3 endPosition, bool enableRenderer)
    {
        if (enableRenderer)
        {
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;

            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;
            if (lineRenderer.enabled)
                lineRenderer.enabled = false;

            return;
        }

        if (lineRenderer.enabled)
        {
            // ✅ Dùng vị trí cố định thay vì vị trí hiện tại của Transform
            Vector3 startPos = fixedStartPos;
            startPos.z = -1f;

            Vector3 endPos = endPosition;
            endPos.z = 0f;

            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }
    }
    public float GetCurrentRopeLength(Vector3 hookEndPosition)
    {
        return Vector3.Distance(fixedStartPos, hookEndPosition);
    }
}
