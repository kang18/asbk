using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public int hp;
    public int touchDmg;
    public float speed;
    public bool dodie;
    public bool whereFloor;
    public bool whereSee;
    public bool isTargetOnRight;
    public int numberOfStalactitesToSpawn = 4;
    public int medusaDmg;
    public float stunTime;
    public float jumpForce;
    public float jumpForceUnder;

    public int minX; // �ִ� ���� X ��
    public bool isminX; // ���� �ִ��� ������ �ִ����� ���� bool ��
    public int maxX; // �ִ� ���� X ��
    public bool ismaxX; // ���� �ִ��� ������ �ִ����� ���� bool ��
    public bool isJump; // �����ϰ� �ִ���
    public bool isUnderJump; // ���������ϰ� �ִ���




    public int activeNumber; // � �ൿ�� �� �������� ���� ���� ��ȣ
    public bool isActive; // �ൿ�� ��ġ�� �ʵ���
    public bool isBress;
    public bool isSnakeAttack;
    public bool isStalactite;
    public bool isMedusa;

    public GameObject player;

    public GameObject pattren;
    public GameObject bressUp;
    public GameObject bressDown;
    public GameObject snakeAttack;
    public GameObject stalactite;
    public Transform[] stalactitePosition;
    public GameObject[] stalactiteAlrm;
    int randomPosition1;
    int randomPosition2;
    int randomPosition3;
    int randomPosition4;
    public GameObject[] medusaEye;

    Animator anim;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(MoveLeftCoroutine());

        Invoke("ActivateBoss", 2f);
    }

    private void ActivateBoss()
    {
        player = GameObject.FindWithTag("Player");
        pattren = GameObject.Find("BossPattern");


        stalactitePosition[0] = GameObject.Find("Position_1").transform;
        stalactitePosition[1] = GameObject.Find("Position_2").transform;
        stalactitePosition[2] = GameObject.Find("Position_3").transform;
        stalactitePosition[3] = GameObject.Find("Position_4").transform;
        stalactitePosition[4] = GameObject.Find("Position_5").transform;
        stalactitePosition[5] = GameObject.Find("Position_6").transform;
        stalactitePosition[6] = GameObject.Find("Position_7").transform;
        stalactitePosition[7] = GameObject.Find("Position_8").transform;
        stalactitePosition[8] = GameObject.Find("Position_9").transform;
        stalactitePosition[9] = GameObject.Find("Position_10").transform;
        stalactitePosition[10] = GameObject.Find("Position_11").transform;

        stalactiteAlrm[0] = GameObject.Find("Position_1_alrm");
        stalactiteAlrm[1] = GameObject.Find("Position_2_alrm");
        stalactiteAlrm[2] = GameObject.Find("Position_3_alrm");
        stalactiteAlrm[3] = GameObject.Find("Position_4_alrm");
        stalactiteAlrm[4] = GameObject.Find("Position_5_alrm");
        stalactiteAlrm[5] = GameObject.Find("Position_6_alrm");
        stalactiteAlrm[6] = GameObject.Find("Position_7_alrm");
        stalactiteAlrm[7] = GameObject.Find("Position_8_alrm");
        stalactiteAlrm[8] = GameObject.Find("Position_9_alrm");
        stalactiteAlrm[9] = GameObject.Find("Position_10_alrm");
        stalactiteAlrm[10] = GameObject.Find("Position_11_alrm");



        foreach (GameObject stalactite in stalactiteAlrm)
        {
            stalactite.SetActive(false);
            SpriteRenderer spriteRenderer3 = stalactite.GetComponent<SpriteRenderer>();
            if (spriteRenderer3 != null)
            {
                Color color = spriteRenderer3.color;
                color.a = 1.0f;
                spriteRenderer3.color = color;
            }
        }
    }

    private void Update()
    {
        UpdateLayer();

        if (!isActive)
        {
            MakeActiveNumber();

            switch (activeNumber)
            {
                case 0:
                    StartCoroutine(MoveRightCoroutine());
                    break;
                case 1:
                    StartCoroutine(MoveLeftCoroutine());
                    break;
                case 2:
                    if (whereFloor)
                    {
                        StartCoroutine(JumpToUnder());
                        break;
                    }
                    else
                    {
                        StartCoroutine(JumpToUpArray());
                        break;
                    }
                case 3:
                    StartCoroutine(SnakeAttack());
                    break;
                case 4:
                    StartCoroutine(Bress());
                    break;
                case 5:
                    StartCoroutine(SpawnStalactites());
                    break;
                case 6:
                    StartCoroutine(Medusa());
                    break;

                default:
                    break;
            }

        }

    }



    private void MakeActiveNumber() // ���� �ൿ ���� ����
    {
        activeNumber = Random.Range(5, 6);

        // 0 - �������� �̵�
        // 1 - ���������� �̵�
        // 2 - ���� || �ϴ� ����  (������ �ִ��� �Ʒ����� �ִ����� ���� �ൿ ����)
        // 3 - ���� 1
        // 4 - ���� 2
        // 5 - ���� 3
        // 6 - ���� 4
    }


    private IEnumerator MoveRightCoroutine() // ���������� �̵��ϴ� �ڷ�ƾ
    {
        isActive = true;

        float moveDuration = Random.Range(0f, 3f);
        float elapsedTime = 0.0f;

        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        while (elapsedTime < moveDuration)
        {
            if (ismaxX)
            {
                isActive = false;
                yield break;
            }
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            JudgmentTransform();
            elapsedTime += Time.deltaTime;

            yield return null;
        }


        yield return new WaitForSeconds(1f);
        isActive = false;
    }

    private IEnumerator MoveLeftCoroutine() // �������� �̵��ϴ� �ڷ�ƾ
    {
        isActive = true;

        float moveDuration = Random.Range(0f, 3f);
        float elapsedTime = 0.0f;

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        while (elapsedTime < moveDuration)
        {
            if (isminX)
            {
                isActive = false;
                yield break;
            }
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            JudgmentTransform();
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        isActive = false;
    }

    private void JudgmentTransform()
    {
        if (transform.position.x <= minX)
        {
            isminX = true;
        }
        else
        {
            isminX = false;
        }

        if (transform.position.x >= maxX)
        {
            ismaxX = true;
        }
        else
        {
            ismaxX = false;
        }
    }// ���� ��ġ�� ���¿��� ������ �Ǵ��ϴ� �Լ�

    IEnumerator JumpToUpArray()
    {
        isActive = true;

        yield return new WaitForSeconds(0.5f);

        isJump = true;
        rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.9f);

        isJump = false;

        yield return new WaitForSeconds(2f);

        isActive = false;


    } // ��� ����

    IEnumerator JumpToUnder() // �ϴ� ����
    {
        isActive = true;

        yield return new WaitForSeconds(0.5f);

        isJump = true;
        isUnderJump = true;
        rigid.AddForce(Vector3.up * jumpForceUnder, ForceMode2D.Impulse);

        yield return new WaitForSeconds(3f);

        isActive = false;
    }




    private void UpdateLayer()
    {
        if(isJump || isUnderJump)
        {
            gameObject.layer = LayerMask.NameToLayer("MonsterRope");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Monster");
        }
    }






    IEnumerator SnakeAttack() // ���߿� �ִϸ��̼ǿ� ���缭 �ڷ�ƾ Ÿ�̹� �����ؾ� ��
    {
        isActive = true;
        isSnakeAttack = true;
        FlipTowardsPlayerDirection();

        yield return new WaitForSeconds(0.5f);

        snakeAttack.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        snakeAttack.SetActive(false);

        isActive = false;
        isSnakeAttack = false;
    }

    IEnumerator Bress() // ���߿� �ִϸ��̼ǿ� ���缭 �ڷ�ƾ Ÿ�̹� �����ؾ� ��
    {
        isActive = true;
        isBress = true;
        FlipTowardsPlayerDirection();

        yield return new WaitForSeconds(0.5f);

        // 1���� ���� ���� 2���� ���� ���� ������ ���ؼ�
        if(whereFloor) { bressDown.SetActive(true); } 
        else { bressUp.SetActive(true); }

        yield return new WaitForSeconds(1.0f);

        if (whereFloor) { bressDown.SetActive(false); } 
        else { bressUp.SetActive(false); }

        isActive = false;
        isBress = false;
    }

    IEnumerator SpawnStalactites()
    {
        isActive = true;
        isStalactite = true;
        RandomPositionCount();

        yield return new WaitForSeconds(0.5f);

        stalactiteAlrm[randomPosition1].SetActive(true);
        stalactiteAlrm[randomPosition2].SetActive(true);
        stalactiteAlrm[randomPosition3].SetActive(true);
        stalactiteAlrm[randomPosition4].SetActive(true);

        yield return new WaitForSeconds(1.2f);

        GameObject stalactite_1 = Instantiate(stalactite, stalactitePosition[randomPosition1].position, Quaternion.identity);
        GameObject stalactite_2 = Instantiate(stalactite, stalactitePosition[randomPosition2].position, Quaternion.identity);
        GameObject stalactite_3 = Instantiate(stalactite, stalactitePosition[randomPosition3].position, Quaternion.identity);
        GameObject stalactite_4 = Instantiate(stalactite, stalactitePosition[randomPosition4].position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        stalactiteAlrm[randomPosition1].SetActive(false);
        stalactiteAlrm[randomPosition2].SetActive(false);
        stalactiteAlrm[randomPosition3].SetActive(false);
        stalactiteAlrm[randomPosition4].SetActive(false);

        yield return new WaitForSeconds(1.0f);

        isActive = false;
        isStalactite = false;
    }
    private void RandomPositionCount() // �������� ������ ���� ����Ʈ 4�� ����
    {
        randomPosition1 = Random.Range(0, 11);
        randomPosition2 = Random.Range(0, 11);
        while(randomPosition1 == randomPosition2)
        {
            randomPosition2 = Random.Range(0, 11);
        }
        randomPosition3 = Random.Range(0, 11);
        while (randomPosition1 == randomPosition3 || randomPosition2 == randomPosition3)
        {
            randomPosition3 = Random.Range(0, 11);
        }
        randomPosition4 = Random.Range(0, 11);
        while (randomPosition1 == randomPosition4 || randomPosition2 == randomPosition4 || randomPosition3 == randomPosition4)
        {
            randomPosition4 = Random.Range(0, 11);
        }

        Debug.Log(randomPosition1);
        Debug.Log(randomPosition2);
        Debug.Log(randomPosition3);
        Debug.Log(randomPosition4);
    }

    IEnumerator Medusa()
    {
        isMedusa = true;
        isActive = true;
        FlipTowardsPlayerDirection();

        yield return new WaitForSeconds(0.2f);

        medusaEye[0].SetActive(true);

        yield return new WaitForSeconds(1.5f);

        medusaEye[0].SetActive(false);
        medusaEye[1].SetActive(true);

        isTargetOnRight = IsPlayerOnRight();

        if (!isTargetOnRight)
        {
            // ���⼭ PlayerBehavior ������Ʈ�� �������� DecreaseHp() �Լ��� ȣ��
            PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();

            if (playerBehavior != null)
            {
                // ������ �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� bool �� ����
                SeeUpdate();
                playerBehavior.SeeUpdate();

                if (whereFloor == playerBehavior.positionUpDown && whereSee == playerBehavior.iswhereSee)
                {
                    playerBehavior.hp -= medusaDmg;
                    StartCoroutine(playerBehavior.Stun(stunTime));
                }
            }
        }

        yield return new WaitForSeconds(1.0f);

        medusaEye[1].SetActive(false);

        yield return new WaitForSeconds(1.0f);

        isMedusa = false;
        isActive = false;
    }
    private void SeeUpdate()
    {
        whereSee = transform.localScale.x < 0;
    }
    private bool IsPlayerOnRight()
    {
        Vector3 playerDirection = player.transform.position - transform.position;

        if (transform.localScale.x > 0) // ���� ������Ʈ�� �������� ���� ���� ��
        {
            return playerDirection.x > 0;
        }
        else // ���� ������Ʈ�� ������ ���� ���� ��
        {
            return playerDirection.x < 0;
        }
    }

    private void FlipTowardsPlayerDirection() // �÷��̾ �ִ� �������� �¿�����ϴ� �Լ�
    {
        Vector3 playerDirection = player.transform.position - transform.position;

        if (playerDirection.x < 0) // �÷��̾ �����ʿ� ���� ��
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (playerDirection.x > 0) // �÷��̾ ���ʿ� ���� ��
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    public SpawnManager spawnManager; // SpawnManager ��ũ��Ʈ�� ������ ������ ����

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (collision.gameObject.CompareTag("Bullet") && !dodie)
        {
            hp -= bullet.dmg;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.DecreaseHp(touchDmg);
        }
    }
}
