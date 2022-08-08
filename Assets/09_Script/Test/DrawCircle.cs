using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer = null; // �~��`�悷�邽�߂� LineRenderer
    [SerializeField] private float m_radius = 0;    // �~�̔��a
    [SerializeField] private float m_lineWidth = 0;    // �~�̐��̑���
    [SerializeField] private float center_x = 0;    // �~�̒��S���W
    [SerializeField] private float center_z = 0;    // �~�̒��S���W
    public int segment = 360;
    private void Reset()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }
    private void Awake()
    {
        int segments = segment;
        m_lineRenderer.startWidth = m_lineWidth;
        m_lineRenderer.endWidth = m_lineWidth;
        m_lineRenderer.positionCount = segments;
        var points = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 380f / segments);
            var x = center_x + Mathf.Sin(rad) * m_radius;
            var z = center_z + Mathf.Cos(rad) * m_radius;
            points[i] = new Vector3(x, 0, z);
        }
        m_lineRenderer.SetPositions(points);
    }
}
