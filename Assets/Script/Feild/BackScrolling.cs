using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScrolling : MonoBehaviour
{
    [Header("�̹��� ����")]
    public Transform[] layers; // �з����� ��� ���̾��

    [Header("��� �̵��ӵ�")]
    public float[] parallaxFactorsX; // �� ��� ���̾��� �¿� �з����� ���
    public float[] parallaxFactorsY; // �� ��� ���̾��� ���Ʒ� �з����� ���


    public Transform playerTransform; // �÷��̾��� Transform ������Ʈ

    public float minY; // Y�� �̵��� ����
    public float maxY; // Y�� �̵��� ����

    public float smoothSpeed; // �з����� ����� �ε巯�� �̵� ����

    private Vector3[] initialPositions; // �ʱ� ��ġ ����
    private Vector3[] targetPositions; // ��ǥ ��ġ ����

    private void Start()
    {
        // �ʱ� ��ġ ����
        initialPositions = new Vector3[layers.Length];
        targetPositions = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            initialPositions[i] = layers[i].position;
            targetPositions[i] = initialPositions[i];
        }
    }

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            // �з����� ����� ��ǥ ��ġ ���
            float parallaxAmountX = playerTransform.position.x * parallaxFactorsX[i] * (-1f);
            float parallaxAmountY = Mathf.Clamp(-playerTransform.position.y * parallaxFactorsY[i], -maxY, -minY); // ���Ʒ� �̵� ������ �ݴ�� �����ϰ� Y�� �̵� ������ ����
            targetPositions[i] = initialPositions[i] + new Vector3(parallaxAmountX, parallaxAmountY, 0f);
        }

        // ������ ����Ͽ� �з����� ��� �ε巴�� �̵�
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].position = Vector3.Lerp(layers[i].position, targetPositions[i], smoothSpeed * Time.deltaTime);
        }
    }
}
