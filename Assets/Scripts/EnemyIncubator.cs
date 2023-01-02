using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncubator : MonoBehaviour
{
    public GameObject[] enemys;
    private float time;// ÿ�����ɼ��
    private float timeInTime;//�������ɼ��
    private float count;//����
    private float countInCount;//ÿ������
    // Start is called before the first frame update
    void Start()
    {
        time = 2;
        timeInTime = 1;
        count = 5;
        countInCount = 4;
        StartCoroutine(CreateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator CreateEnemy()
    {
        for(int i=0;i<count;i++)
        {
            for(int j = 0; j < countInCount; j++)
            {
                Instantiate(enemys[Random.Range(0, enemys.Length)],transform.position,Quaternion.identity);
                yield return new WaitForSeconds(timeInTime);
            }
            yield return new WaitForSeconds(time);
        }
    }
}
