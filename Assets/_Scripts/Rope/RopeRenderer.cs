using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _segments = 10;

    public void DrawLine(Vector3 a, Vector3 b, float length)
    {
        _lineRenderer.enabled = true;

        float halfDevider = 2f;
        float interpolant = Vector3.Distance(a, b) / length;
        float offset = Mathf.Lerp(length / halfDevider, 0f, interpolant);

        Vector3 aDown = a + Vector3.down * offset;
        Vector3 bDown = b + Vector3.down * offset;

        _lineRenderer.positionCount = _segments + 1;

        for (int i = 0; i < _lineRenderer.positionCount; i++)
            _lineRenderer.SetPosition(
                i, Bezier.GetPoint(a, aDown, bDown, b, i / (float)_segments)
                );
    }

    public void HideLine()
    {
        _lineRenderer.enabled = false;
    }
}
