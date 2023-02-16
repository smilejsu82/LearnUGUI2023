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
        //신규 유저, 기존 유저 
        //변하지 않는 게임 데이터 반드시 로드 필요 
        DataManager.instance.LoadItemData();

        //신규유저, 기존 유저 판별 
        Debug.Log(Application.persistentDataPath);
        string path = string.Format("{0}/inventory_info.json", Application.persistentDataPath);
        Debug.LogFormat("<color=cyan>{0}</color>", path);
        //inventory_info.json 있는지 판별 
        Debug.LogFormat("Exists: {0}", File.Exists(path));
        if (File.Exists(path))
        {
            //있으면 : 기존유저 -> 역직렬화 -> InfoManager.instance.inventoryInfo
            Debug.Log("<color=yellow>기존유저</color>");

            //불러오기 
            string json = File.ReadAllText(path);

            //역직렬화 
            var inventoryInfo = JsonConvert.DeserializeObject<InventoryInfo>(json);

            Debug.LogFormat("inventoryInfo: {0}", inventoryInfo);
            Debug.LogFormat("inventoryInfo.itemInfos.Count: {0}", inventoryInfo.itemInfos.Count);//0

            //InfoManager에 저장 
            InfoManager.instance.InventoryInfo = inventoryInfo;
        }
        else 
        {
            Debug.Log("<color=yellow>신규유저</color>");
            //없으면 : 신규유저 -> info만들어서 파일 inventory_info.json 저장 -> InfoManager.instance.inventoryInfo
            var inventoryInfo = new InventoryInfo();
            inventoryInfo.Init();

            //직렬화 (객체->문자열) 
            string json = JsonConvert.SerializeObject(inventoryInfo);
            Debug.Log(json);

            //파일 저장 
            File.WriteAllText(path, json);
            Debug.Log("save complete");

            //InfoManager에 저장 
            InfoManager.instance.InventoryInfo = inventoryInfo;
        }


        this.director.Init();
    }
}
