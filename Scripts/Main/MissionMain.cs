using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionMain : MonoBehaviour
{
    public UIMissionDirector director;
    
    void Start()
    {
        DataManager.instance.LoadData<MissionData>();
        DataManager.instance.LoadData<RewardItemData>();

        if (InfoManager.instance.IsNewbie())
        {
            InfoManager.instance.Init();
        }
        else 
        {
            InfoManager.instance.LoadMissionInfos();
        }


        this.director.Init();
    }
}
