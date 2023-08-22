using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp;
    public int touchdmg;
    public float speed = 5f;
    public bool dodie; // �׾����� ��Ҵ���
    public bool id; // �������� ���ʰ� ������ �� ��� ������ �ٶ󺸰� �ִ����� ���� ��������Ʈ�� ������ ���ؼ�
    
    // �ǰݽ� �������� ���� ������
    public Color hitColor; // �ǰݽ� �������� ����
    public float hitDuration = 0.1f; // ������ �ٲ�� �ð�
    private Renderer renderer;
    private Color originalColor;

    public SpawnManager spawnManager; // SpawnManager ��ũ��Ʈ�� ������ ������ ����
    public GameObject gem;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        if (id) // ��������Ʈ�� �ݴ�� �Ǿ� �ִ� �͵� ������ ��
        {
            // ������Ʈ�� X �������� ������Ŵ
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        spawnManager = FindObjectOfType<SpawnManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (collision.gameObject.CompareTag("Bullet") && !dodie)
        {
            StartCoroutine(OnDamage());
            hp -= bullet.dmg;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();

        if (playerBehavior != null)
        {
            playerBehavior.DecreaseHp(touchdmg);
        }

    }

    public IEnumerator OnDamage()
    {
        renderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        renderer.material.color = originalColor;
    }
 
    private void OnDisable()
    {
        spawnManager.catchMonsters++;
    }
}

