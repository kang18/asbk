using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_test : Monster
{
    public bool findAttack; // ���� ã��
    public bool isAttack; // ���� ��
    public float rayLength = 2f; // ���� ����

    public float xposition;
    public float yposition;

    public BoxCollider2D attackArea; // ���� ����

    // # �ִϸ��̼� ���� ����
    Animator anim;
    private bool originalIsWalk;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hp <= 0 && !dodie)
        {
            StartCoroutine(Die());
        }
        if (!dodie)
        {
            if (!isAttack)
            {
                if (!findAttack)
                {
                    MoveMonster();
                }
                else
                {
                    StartCoroutine(Attack());
                }
            }
            ScanFront();
        }
    }

    private void MoveMonster() // �������� �̵��ϴ� �Լ�
    {
        anim.SetBool("isWalk", true);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void ScanFront() // ���濡 �÷��̾�, Ȥ�� �Ͽ콺�� �ִ� Ȯ���ϴ� �Լ�
    {
        Vector2 raycastOrigin = new Vector2(transform.position.x - xposition, transform.position.y - yposition);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.left, rayLength, LayerMask.GetMask("Player") | LayerMask.GetMask("House"));

        Debug.DrawRay(raycastOrigin, Vector2.left * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("House"))
            {
                anim.SetBool("isWalk", false);
                findAttack = true;
            }
        }
        else
        {
            findAttack = false;
            if(!isAttack)
            {
                anim.SetBool("isWalk", true);
            }
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        attackArea.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackArea.enabled = false;


        yield return new WaitForSeconds(0.55f);

        anim.SetBool("isAttack", false);

        yield return new WaitForSeconds(2f);

        isAttack = false;
        
    }


  

    IEnumerator Die()
    {
        dodie = true;
        anim.SetTrigger("doDie");

        // �ݶ��̴� ��Ȱ��ȭ
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        // Rigidbody2D ������ ��Ȱ��ȭ
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        rigidbody.simulated = false;

        yield return new WaitForSeconds(0.7f);
        anim.enabled = false;
        if (Random.Range(0f, 100f) <= 30f)
        {
            GameObject newObj = Instantiate(gem, transform.position, Quaternion.identity);
        }


        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}