using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ϴ� ������ (���� ��ü)
public class InventoryInfo 
{
    public List<ItemInfo> itemInfos;    //�÷��� �ݵ�� ����� �ν��Ͻ�ȭ 

    /// <summary>
    /// Called only for new users
    /// </summary>
    //ó�� �����ɶ� (�츮�� ��ü�� �����)
    public void Init()
    {
        this.itemInfos = new List<ItemInfo>();  //�ű����� �ϰ�츸 ȣ�� 
    }

    //����Ǿ� �ִ� json������ �ҷ��ͼ� json ���ڿ��� ������ȭ �ɶ� (Newton -> ��ü�� ����)
}
