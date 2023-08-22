using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : Monster
{
    public float rayLength; // ���� ����
    public float bulletSpeed; // �Ѿ� ���ư��� �ӵ�

    public GameObject bombArray; // ���� ����


    private void Update()
    {
        if (hp <= 0 && !dodie)
        {
            StartCoroutine(Bomb());
        }
        if (!dodie)
        {
            MoveMonster();
        }
    }

    private void MoveMonster() // �������� �̵��ϴ� �Լ�
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    private IEnumerator Bomb()
    {
        dodie = true;

        Renderer renderer = GetComponent<Renderer>();
        Color startColor = renderer.material.color;
        Color targetColor = new Color(1f, 0.5f, 0f);
        float duration = 2f; // ��ȭ�� �ɸ��� �ð�
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // ������ ���
            renderer.material.color = Color.Lerp(startColor, targetColor, t); // �����Ͽ� ���� ����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = targetColor; // ���� ���� ����
        bombArray.SetActive(true);
       
        yield return new WaitForSeconds(1f);
        if (Random.Range(0f, 100f) <= 30f)
        {
            GameObject newObj = Instantiate(gem, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // ������Ʈ ����
    }
}


