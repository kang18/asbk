using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float maxLineLength = 10f;
    public LayerMask collisionMask;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // ������ ������
        Vector3 lineStartPoint = transform.position;

        // �浹 ������ ���� ���� �ʱ�ȭ
        RaycastHit2D hit;

        // �ݶ��̴��� �浹�ϱ� �������� �Ÿ� ���
        float lineLength = maxLineLength;
        hit = Physics2D.Raycast(lineStartPoint, Vector2.up, maxLineLength, collisionMask);
        if (hit.collider != null)
        {
            lineLength = hit.distance;
        }

        // ������ ���� ���
        Vector3 lineEndPoint = lineStartPoint + Vector3.up * lineLength;

        // ���� �������� ����Ʈ�� ����
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, lineStartPoint);
        lineRenderer.SetPosition(1, lineEndPoint);
    }

}
