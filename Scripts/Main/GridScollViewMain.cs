using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GridScollViewMain : MonoBehaviour
{
    public UIGridScrollViewDirector director;

    void Start()
    {
        //�ű� ����, ���� ���� 
        //������ �ʴ� ���� ������ �ݵ�� �ε� �ʿ� 
        DataManager.instance.LoadItemData();

        //�ű�����, ���� ���� �Ǻ� 
        Debug.Log(Application.persistentDataPath);
        string path = string.Format("{0}/inventory_info.json", Application.persistentDataPath);
        Debug.LogFormat("<color=cyan>{0}</color>", path);
        //inventory_info.json �ִ��� �Ǻ� 
        Debug.LogFormat("Exists: {0}", File.Exists(path));
        if (File.Exists(path))
        {
            //������ : �������� -> ������ȭ -> InfoManager.instance.inventoryInfo
            Debug.Log("<color=yellow>��������</color>");

            //�ҷ����� 
            string json = File.ReadAllText(path);

            //������ȭ 
            var inventoryInfo = JsonConvert.DeserializeObject<InventoryInfo>(json);

            Debug.LogFormat("inventoryInfo: {0}", inventoryInfo);
            Debug.LogFormat("inventoryInfo.itemInfos.Count: {0}", inventoryInfo.itemInfos.Count);//0

            //InfoManager�� ���� 
            InfoManager.instance.InventoryInfo = inventoryInfo;
        }
        else 
        {
            Debug.Log("<color=yellow>�ű�����</color>");
            //������ : �ű����� -> info���� ���� inventory_info.json ���� -> InfoManager.instance.inventoryInfo
            var inventoryInfo = new InventoryInfo();
            inventoryInfo.Init();

            //����ȭ (��ü->���ڿ�) 
            string json = JsonConvert.SerializeObject(inventoryInfo);
            Debug.Log(json);

            //���� ���� 
            File.WriteAllText(path, json);
            Debug.Log("save complete");

            //InfoManager�� ���� 
            InfoManager.instance.InventoryInfo = inventoryInfo;
        }


        this.director.Init();
    }
}
