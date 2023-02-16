using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDailyInfo 
{
    public int id;          //daily_reward_data ÀÇ id 
    public int state;     //È¹µæ ¿©ºÎ , 0 : ¾È¸ÔÀ½ , 1: ¸ÔÀ½ 
    public string date;  //2022-02-16 

    //»ý¼ºÀÚ 
    public RewardDailyInfo(int id)
    {
        this.id = id;
    }
}
