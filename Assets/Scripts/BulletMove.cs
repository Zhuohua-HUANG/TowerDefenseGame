using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public GameObject target = null;
    public TowerAI scripts=null;
    private float times;
    void Start()
    {
        times = 3;
    }

    // Update is called once per frame
    void Update()
    {
        times-= Time.deltaTime;
        if (times <=0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward*Time.deltaTime*15);
        Attack();
    }

    private void Attack()
    {
        if(target != null)
        {
            if(Vector3.Distance(transform.position,target.transform.position) < 1f)
            {
                Destroy(target);
                scripts.enemy.Remove(target);
                Destroy(gameObject);
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
