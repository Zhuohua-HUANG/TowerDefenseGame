using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> enemy;// a list for tower to attack
    private GameObject targetObject;// target monster
    private float distance;
    private Transform turret; //the turret to turn around
    private float times;
    private GameObject bulletPrefab;
    private Transform firePos;

    void Start()
    {
        times = 0;
        distance = 1000;
        targetObject = null;
        enemy= new List<GameObject>();
        turret= transform.GetChild(0).GetChild(0);
        firePos=turret.GetChild(0).GetChild(0);
        bulletPrefab = Resources.Load<GameObject>("Muzzle_1");
    }

    void Update()
    {
/*        Debug.Log(enemy.Count);*/
        if (enemy.Count > 0)
        {
            if (targetObject == null)
            {
                //select monster
                targetObject = SelectTarget();
            }
        }
        if (targetObject!= null)
        {
            // pre-attack
            LookTarget();
        }
    }

    public void OnTriggerEnter(Collider other)//monster enter attack range
    {
        if (other.tag == "Enemy")
        {
            if(!enemy.Contains(other.gameObject))
            {
                enemy.Add(other.gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other) // monster exit attack range
    {
        if (other.tag == "Enemy")
        {
            if (targetObject!= null && other.name == targetObject.name)
            {
                targetObject = null;
            }
            if (enemy.Contains(other.gameObject))
            {
                enemy.Remove(other.gameObject);
            }
        }
    }

    private GameObject SelectTarget()
    {
        GameObject temp = null;
        float tempDistance = 0;
        for(int i =0; i < enemy.Count;i++)
        {
            tempDistance=Vector3.Distance(transform.position, enemy[i].transform.position);
            if(tempDistance<=distance)
            {
                distance= tempDistance;
                temp= enemy[i];
            }
        }
        return temp;
    }

    private void LookTarget()
    {
        Vector3 pos= targetObject.transform.position; 
        pos.y=turret.transform.position.y;
        turret.LookAt(pos);
        times += Time.deltaTime;
        if(times>= 1)
        {
            // processing attack
            Attack();
            times = 0;
        }
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
        bullet.AddComponent<BulletMove>().target=targetObject;
        bullet.transform.LookAt(targetObject.transform.position);
        bullet.GetComponent<BulletMove>().scripts = this;
    }
}
