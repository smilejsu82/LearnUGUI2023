using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ϴ� ������ (����ȭ �Ǿ� ����Ǵ� ��ü)
public class ItemInfo 
{
    public int id;
    public int amount;
    //������ 
    public ItemInfo(int id, int amount = 1)
    {
        this.id = id;
        this.amount = amount;
    }
}
