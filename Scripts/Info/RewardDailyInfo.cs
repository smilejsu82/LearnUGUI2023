using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDailyInfo 
{
    public int id;          //daily_reward_data �� id 
    public int state;     //ȹ�� ���� , 0 : �ȸ��� , 1: ���� 
    public string date;  //2022-02-16 

    //������ 
    public RewardDailyInfo(int id)
    {
        this.id = id;
    }
}
