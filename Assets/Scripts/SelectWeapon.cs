using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectWeapon : MonoBehaviour
{
    private GameObject selectPanel;
    private GameObject firstPanel;
    private GameObject nextSelectPanel;
    public GameObject[] towers;
    private GameObject selectTower; // tower that ready to create
    private Transform basePos; // tower base
    // Start is called before the first frame update
    void Start()
    {
        selectTower = null;
        selectPanel = transform.Find("Canvas").gameObject;
        firstPanel = selectPanel.transform.GetChild(0).gameObject;
        nextSelectPanel = selectPanel.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectBase();
        }
    }

    private void SelectBase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//射线，用于确认点击物体
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 200))
        {
            //如果射线击中collision的物体
            //Physics.Raycast(Ray, out hitInfo, range, mask)
            /* 参数说明
                Ray：射线
                    Ray.origin表示发射位置，Ray.direction表示发射方向
                hitInfo：被击中的物体信息
                    通过hit.collider.tag判断物体，可用hitInfo.point获得击中点坐标，可通过hitInfo.transform获取物体，进而获取物体等信息如：hitInfo.transform.gameObject.layer（或者用hitInfo.collider.gameObject）
                range：射线距离（没填就是不限距离）
                mask：射线蒙版，表示击中了哪个layer，这里虽然mask是int属性，但是这个参数不能直接填数字，要用如下写法，否则无法检测到东西（没填就指检测所有层）
            */
            Debug.Log("I collided.");
            if (hit.transform.tag == "TowerBase" && EventSystem.current.IsPointerOverGameObject()==false)
            {
                //如果点击正确物体
                Debug.Log("I touch the TowerBase.");
                basePos= hit.transform;
                ShowSelectPanel();
            }
            else
            {
                Debug.Log("I touch the wrong place.");
            }
        }
    }
    
    private void ShowSelectPanel()
    {
        //面板显示函数
        //selectPanel.transform.parent = pos;//面板位置确定
        selectPanel.transform.SetParent(basePos, false);
        selectPanel.transform.localPosition = new Vector3(0, 8, 4);
        selectPanel.SetActive(true);//设置面板打开
        if(basePos.childCount >= 2)
        {
            firstPanel.SetActive(false);
            nextSelectPanel.SetActive(true);
        }
        else
        {
            firstPanel.SetActive(true);
            nextSelectPanel.SetActive(false);
        }
    }

    // 事件
    public void SelectTowerOne(bool isOn)
    {
        if(isOn)
        {
            Debug.Log("SelectTowerOne");
            selectTower = towers[0];
            firstPanel.SetActive(false);
            nextSelectPanel.SetActive(true);
        }
    }

    public void SelectTowerTwo(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("SelectTowerTwo");
            selectTower = towers[1];
            firstPanel.SetActive(false);
            nextSelectPanel.SetActive(true);
        }
    }
    public void SelectTowerThree(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("SelectTowerThree");
            selectTower = towers[2];
            firstPanel.SetActive(false);
            nextSelectPanel.SetActive(true);
        }
    }

    public void CloseAll()// 关闭所有
    {
        selectPanel.SetActive(false);
        nextSelectPanel.SetActive(false);
        firstPanel.SetActive(true);
    }

    public void CloseNext()
    {
        nextSelectPanel.SetActive(false);
        firstPanel.SetActive(true);
    }

    public void CreateTower()
    {
        if(basePos.childCount >= 2)
        {
            Debug.Log("Tower already exit, sir!");
        }
        else
        {
            Debug.Log("Creating now, sir!");
            nextSelectPanel.SetActive(false);
            GameObject tempTower = Instantiate(selectTower);
            tempTower.transform.SetParent(basePos, false);
            tempTower.transform.localPosition = Vector3.up * 2.5f;
            //炮塔攻击
            tempTower.AddComponent<TowerAI>();
            CloseAll();
        }
    }

    public void SaleTower()
    {
        Debug.Log("Saling now, sir!");
        if (basePos.childCount >=2)
        {
            Destroy(basePos.GetChild(0).gameObject);
            CloseAll();
        }
        else
        {
            Debug.Log("No tower to sale, sir!");
        }
    }

    private void InitUI()
    {
        selectTower = null;
        firstPanel.SetActive(true);
        nextSelectPanel.SetActive(false);
    }
}
