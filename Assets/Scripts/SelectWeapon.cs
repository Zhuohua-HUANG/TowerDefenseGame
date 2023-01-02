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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//���ߣ�����ȷ�ϵ������
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 200))
        {
            //������߻���collision������
            //Physics.Raycast(Ray, out hitInfo, range, mask)
            /* ����˵��
                Ray������
                    Ray.origin��ʾ����λ�ã�Ray.direction��ʾ���䷽��
                hitInfo�������е�������Ϣ
                    ͨ��hit.collider.tag�ж����壬����hitInfo.point��û��е����꣬��ͨ��hitInfo.transform��ȡ���壬������ȡ�������Ϣ�磺hitInfo.transform.gameObject.layer��������hitInfo.collider.gameObject��
                range�����߾��루û����ǲ��޾��룩
                mask�������ɰ棬��ʾ�������ĸ�layer��������Ȼmask��int���ԣ����������������ֱ�������֣�Ҫ������д���������޷���⵽������û���ָ������в㣩
            */
            Debug.Log("I collided.");
            if (hit.transform.tag == "TowerBase" && EventSystem.current.IsPointerOverGameObject()==false)
            {
                //��������ȷ����
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
        //�����ʾ����
        //selectPanel.transform.parent = pos;//���λ��ȷ��
        selectPanel.transform.SetParent(basePos, false);
        selectPanel.transform.localPosition = new Vector3(0, 8, 4);
        selectPanel.SetActive(true);//��������
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

    // �¼�
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

    public void CloseAll()// �ر�����
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
            //��������
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
