using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class RewardDailyMain : MonoBehaviour
{

    public UIRewardDailyDirector director;

    void Start()
    {
        DataManager.instance.LoadRewardDailyData();
        DataManager.instance.LoadCurrencyData();
        DataManager.instance.LoadRewardItemData();
        InfoManager.instance.Init();

        if (this.IsNewbie())
        {
            Debug.Log("신규유저");
            //RewardDailyInfo들 (30개) 을 만들어서 저장 
            //[
            //  { "id" : 1000, "state" : 0, "date" : null }, { "id" : 1000, "state" : 0, "date" : null }...
            //]
            foreach (var data in DataManager.instance.GetDailyRewardDatas())
            {
                var info = new RewardDailyInfo(data.id);
                InfoManager.instance.RewardDailyInfos.Add(info);
            }

            Debug.LogFormat("{0}", Application.persistentDataPath);
            //저장 
            InfoManager.instance.SaveRewardDailyInfos();

        }
        else
        {
            Debug.Log("기존유저");
            //파일 불러오기 
            InfoManager.instance.LoadRewardDailyInfos();
            //UI에 보여주기 
        }

        this.director.Init();

    }

    private bool IsNewbie()
    {
        var path = string.Format("{0}/daily_reward_info.json", Application.persistentDataPath);
        if (File.Exists(path))
            return false;
        else 
            return true;
    }

}
