using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIRewardDailyDirector : MonoBehaviour
{
    public GameObject cellviewPrefab;
    public Transform gridTrans;
    public Button btnClaim;

    private List<UIRewardDailyCellView> list;
    private int idx = 0;    //현재위치 

    public void Init()
    {
        this.list = new List<UIRewardDailyCellView>();

        this.btnClaim.onClick.AddListener(() => {


            //저장된 데이터에서 (reward_daily_info.json)
            //현재 날짜가 있나? 2022-02-16  (같은게 있나?)
            var strNow = DateTime.Now.ToString("yyyy/MM/dd");
            if (InfoManager.instance.RewardDailyInfos.Any(x => x.date == strNow))
            {
                //만약에 현재 날짜가 이미 저장되어 있다면, 난 이미 오늘 보상을 받았음 
                Debug.Log("이미 보상을 받았습니다.");
                return;
            }


            Debug.Log(this.idx);

            var cellview =this.list[this.idx];
            cellview.Claim();
            //RewardDailyInfo의 state를 1로 변경하고 date를 현재 날짜로 변경 하고 
            var foundInfo = InfoManager.instance.RewardDailyInfos.Find(x => x.id == cellview.id);
            foundInfo.state = 1;
            foundInfo.date = strNow;

            //저장 
            InfoManager.instance.SaveRewardDailyInfos();


            this.idx++;

            if (this.idx > DataManager.instance.GetDailyRewardCount() - 1)
            {
                this.idx = 0;

                //리셋 
                for (int i = 0; i < InfoManager.instance.RewardDailyInfos.Count; i++) {
                    var info = InfoManager.instance.RewardDailyInfos[i];
                    info.state = 0;
                    info.date = null;
                }
                //저장 
                InfoManager.instance.SaveRewardDailyInfos();

                this.ResetCellviewList();
            }

            this.list[this.idx].Focus();    //다음 

        });

        for (int i = 0; i < DataManager.instance.GetDailyRewardCount(); i++)
        {
            var go = Instantiate(this.cellviewPrefab, this.gridTrans);
            var cellview = go.GetComponent<UIRewardDailyCellView>();

            this.list.Add(cellview);

            var key = i + 1000;
            var data = DataManager.instance.GetDailyRewardData(key);

            var atlas = AtlasManager.instance.GetAtlasByName("UIRewardDaily");
            Sprite sp = null;

            switch ((UIEnums.eRewardItemType)data.type)
            {
                case UIEnums.eRewardItemType.RewardItem:
                    var rewardItemData = DataManager.instance.GetRewardItemData(data.reward_id);
                    sp = atlas.GetSprite(rewardItemData.sprite_name);
                    break;

                case UIEnums.eRewardItemType.Currency:
                    var currencyData = DataManager.instance.GetCurrencyData(data.reward_id);
                    sp = atlas.GetSprite(currencyData.sprite_name);
                    break;
            }

            //reward daily info 정보로 cellview 업데이트 
            var info = InfoManager.instance.GetRewardDailyInfo(data.id);
            cellview.Init(data.id, data.day, data.amount, sp, info.state);
        }

        //초기화 (첫 cellview를 선택)
        this.UpdateIndex();
        this.list[this.idx].Focus();

    }

    private void UpdateIndex()
    {
        this.idx = 
            InfoManager.instance.RewardDailyInfos.Where(x => x.date != null).Count();   
    }


    private void ResetCellviewList()
    {
        foreach (var cellview in this.list)
        {
            cellview.ResetUI();
        }
    }

}
