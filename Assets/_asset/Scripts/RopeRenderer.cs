using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Transform startPosition;

    private float Line_width = 5f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = Line_width;

        lineRenderer.enabled = false;
    }
    public void RenderLine(Vector3 endPosition, bool enableRenderer)
    {
        if (enableRenderer)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                
            }
            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }
        if (lineRenderer.enabled)
        {
            Vector3 startPos = startPosition.position;
            startPos.z = -1f;

            Vector3 endPos = endPosition;
            endPos.z = 0f;

            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
            
        }
    }
}
