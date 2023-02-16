using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//변하는 데이터 (직렬화 되어 저장되는 객체)
public class ItemInfo 
{
    public int id;
    public int amount;
    //생성자 
    public ItemInfo(int id, int amount = 1)
    {
        this.id = id;
        this.amount = amount;
    }
}
