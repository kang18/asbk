using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : Monster
{
    public bool findAttack; // ���� ã��
    public bool isAttack; // ���� ��
    public float rayLength; // ���� ����
    public float bulletSpeed; // �Ѿ� ���ư��� �ӵ�

    public GameObject flyingBullet; // �߻��� ���� ������
    public Transform flyingTransform; // �߻��� ���� ��ġ
    public GameObject step; // ���߿� �� �ִ� ������ �ֱ� ���� ����

    //Animator anim;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!dodie)
        {
            if (!isAttack && !findAttack)
            {
                MoveMonster();
            }
            ScanFront();
        }

        if (hp <= 0 && !dodie)
        {
            StartCoroutine(Die());
        }
    }




    private void MoveMonster() // �������� �̵��ϴ� �Լ�
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    private void ScanFront() // ���濡 �÷��̾�, Ȥ�� �Ͽ콺�� �ִ� Ȯ���ϴ� �Լ�
    {
        float yOffset = -3.0f; // Y ��ǥ�� ������ ��

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, yOffset, 0), Vector2.left,
            rayLength, LayerMask.GetMask("Player") | LayerMask.GetMask("House") | LayerMask.GetMask("PlayerOnDamage"));

        Debug.DrawRay(transform.position + new Vector3(0, yOffset, 0), Vector2.left * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("House"))
            {
                findAttack = true;
                if (!isAttack)
                {
                    StartCoroutine(FireSoundWave());
                }
            }
        }
        else
        {
            findAttack = false;
        }
    }


    //IEnumerator FireBullet(Vector3 targetPosition)
    //{
    //    //anim.SetBool("isAttack", true);

    //    isAttack = true;
    //    yield return new WaitForSeconds(0.7f);

    //    // �߻��� �Ѿ� ����
    //    GameObject bullet = Instantiate(flyingBullet, transform.position, Quaternion.identity);

    //    // �Ѿ��� ���� ����
    //    Vector3 direction = (targetPosition - transform.position).normalized;


    //    bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    //    yield return new WaitForSeconds(0.2f);



    //    //anim.SetBool("isAttack", false);
    //    isAttack = false;
    //    //yield return new WaitForSeconds(0.2f); // ���� ����

    //}


    IEnumerator FireSoundWave()
    {
        isAttack = true;

        while (findAttack)
        {
            // �߻��� �Ѿ� ����
            GameObject bullet = Instantiate(flyingBullet, flyingTransform.position, Quaternion.identity);

            // �Ѿ��� ���� ���� (���� ����)
            Vector3 direction = Vector3.left; // Ȥ�� ���ϴ� ���� �������� ����

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.2f);

            // ��� ������
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            yield return new WaitForSeconds(0.8f);
        }

        isAttack = false;
    }








    IEnumerator Die()
    {
        dodie = true;
        //anim.SetTrigger("doDie");
        step.SetActive(false);

        // �ݶ��̴� ��Ȱ��ȭ
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        // Rigidbody2D ������ ��Ȱ��ȭ
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        rigidbody.simulated = false;

        yield return new WaitForSeconds(1f);  // ���� �Լ� �۵� ��, ������Ʈ�� �����Ǵ� �ð�, �ִϸ��̼� ��� �ð��̶� ����� ��
        if (Random.Range(0f, 100f) <= 30f)
        {
            GameObject newObj = Instantiate(gem, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
