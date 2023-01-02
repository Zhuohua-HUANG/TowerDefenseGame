using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncubator : MonoBehaviour
{
    public GameObject[] enemys;
    private float time;// 每波生成间隔
    private float timeInTime;//波内生成间隔
    private float count;//波数
    private float countInCount;//每波数量
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
