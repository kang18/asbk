using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Color hitColor;  // �ǰݽ� �������� ����
    public float hitDuration = 0.1f;  // ������ �ٲ�� �ð�

    private Renderer objectRenderer;
    private Color originalColor;

    public PlayerBehavior player;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    public void TakeDamage(int dmg) // ������ �����ϴ� �ڷ�ƾ �Լ� ����
    {
        if(player.hp > 0)
        {
            player.hp -= dmg;
        }
        
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        objectRenderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        objectRenderer.material.color = originalColor;
    }
}
