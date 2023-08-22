using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int attackDamage; // ���ݷ�
    public int bulletNumber; // ���� �߻��ϴ� �Ѿ��� ��ȣ��(�Ѿ� ����)
    public float attackSpeed; // ���� ���ǵ�
    private float attackTimer = 0f;
    public bool positionUpDown;  // �÷��̾ ���� �ִ��� �Ʒ��� �ִ���


    private bool keyUp; // �� ����Ű�� ���ȴ���
    private bool keyDown; // �Ʒ� ����Ű�� ���ȴ���
    private bool isAttack; // ���� ���� ������


    public Transform positionUp;
    public Transform positionDown;
    public Transform bulletPosition;
    public GameObject[] bullet; // �Ѿ� ������ ���� �迭


    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
        Move();
    }

    private void FixedUpdate()
    {
        attackTimer += Time.fixedDeltaTime;
        if (attackTimer >= attackSpeed)
        {
            Attack();
            attackTimer = 0f;
        }
    }


    private void GetInput()  // Ű �Է� �޴� �Լ�
    {
        keyUp = Input.GetKey(KeyCode.UpArrow);
        keyDown = Input.GetKey(KeyCode.DownArrow);
        isAttack = Input.GetButton("Attack");
    }

    private void Move() // �÷��̾� ��ġ �̵� �Լ�
    {
        if (keyUp && !positionUpDown)
        {
            transform.position = positionUp.position;
            positionUpDown = true;
        }

        if (keyDown && positionUpDown)
        {
            transform.position = positionDown.position;
            positionUpDown = false;
        }
    }

    private void Attack() // �Ѿ� �߻� �Լ�
    {
        if (isAttack)
        {
            GameObject shotbullet = Instantiate(bullet[bulletNumber], bulletPosition.position, Quaternion.identity);

            shotbullet.transform.localScale = new Vector3(-shotbullet.transform.localScale.x, shotbullet.transform.localScale.y, shotbullet.transform.localScale.z);

            Rigidbody2D projectileRigidbody = shotbullet.GetComponent<Rigidbody2D>();
            if (projectileRigidbody != null)
            {
                projectileRigidbody.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            }
        }
    }
}
