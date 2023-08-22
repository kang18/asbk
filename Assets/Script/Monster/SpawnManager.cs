using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public int waveCnt;

    private float spawnTimer = 0f;
    public float nextSpawnDelay;

    public List<SpawnTXT> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public bool readList; // List�� �а� �ִ��� �Ǵ�

    public int catchMonsters; // ���� ���� ��
    public bool waveClear; // �ش� Wave�� Ŭ���� �ߴ��� �Ǵ�

    public GameObject[] Monsters; // ���͸� ���� �迭
    public Transform[] spawnPositionUpDown; // ���� �Ʒ���


    public TMP_Text tmp;
    public TMP_Text wave;

    private void Awake()
    {
        spawnList = new List<SpawnTXT>();
        ReadSpaenTXT();
    }

    private void Update()
    {
        // #. UI ����
        UiControl();

        // ���߿� ���� �������� �������� �� �̻� ���� �ؽ�Ʈ ���� �� �е��� �����ؾ� ��
        if (GameManager.Instance.IsPaused) // GameManager�� IsPaused�� üũ�Ͽ� �Ͻ����� ������ Ȯ��
        {
            spawnTimer = 0f; // �Ͻ����� �߿��� ���� Ÿ�̸Ӹ� �����Ͽ� ������ ����
            return; // �Ͻ����� ���̸� ���� ������ �������� ����
        }

        if (spawnEnd && catchMonsters == spawnList.Count)
        {
            catchMonsters = 0;
            spawnEnd = false;
            readList = true;
            waveCnt++;
            Invoke("ReadSpaenTXT", 3f);
        }

        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= nextSpawnDelay && !spawnEnd && !readList)
        {
            SpawnMonster();
            spawnTimer = 0f;
        }
    }




    private void UiControl()
    {
        tmp.text = "Wave" + waveCnt.ToString();
        wave.text = "Max Wave : " + waveCnt.ToString();
    }



    void ReadSpaenTXT()
    {
        // #1.���� �ʱ�ȭ
        spawnList.Clear(); // �ؽ�Ʈ ������ �б� �� ����Ʈ�� �ʱ�ȭ
        spawnIndex = 0; // ù��°���� �о�� �ϴϱ�


        // #2.���� �ʱ�ȭ
        TextAsset textFile = Resources.Load("Wave" + waveCnt) as TextAsset; // as TextAsset�� �߰��������ν� �ؽ�Ʈ ������ �´��� �ƴ��� �� �� �� ����
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine(); // �ؽ�Ʈ �����͸� �� �پ� ��ȯ�ϴ� �Լ�
            Debug.Log(line);

            if (line == null)
                break;

            // #3.������ ������ ����
            SpawnTXT spawnData = new SpawnTXT();
            spawnData.delay = float.Parse(line.Split(',')[0]);  // float.Parse(@@) ��ȣ ���� ���� �Ľ� 
            spawnData.type = line.Split(',')[1];                // Split Ư�� ���ڿ��� �������� ��� �ؽ�Ʈ ���� �б�
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // #.�ؽ�Ʈ ���� �ݱ�
        stringReader.Close();

        // #.ù��° ���� ������ ����
        nextSpawnDelay = spawnList[0].delay;

        readList = false;
    }


    private void SpawnMonster()
    {
        int enemyIndex = 0;

        switch (spawnList[spawnIndex].type)
        {
            case "Basic":
                enemyIndex = 0;
                break;

            case "Flying":
                enemyIndex = 1;
                break;

            case "Jump":
                enemyIndex = 2;
                break;

            case "Stone":
                enemyIndex = 3;
                break;

            case "Boss":
                enemyIndex = 4;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;

        GameObject monster = Instantiate(Monsters[enemyIndex], spawnPositionUpDown[enemyPoint].position, Quaternion.identity);

        // #.������ �ε��� ����
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // #.���� ������ ������ ����
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

}
