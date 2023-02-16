using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMain : MonoBehaviour
{
    
    public UIShop uiShop;

    void Start()
    {
        DataManager.instance.LoadShopData();    //������ �ε� 

        EventManager.instance.onBtnAdClick = () => {
            Debug.Log("���� �����ݴϴ�.");
        };

        EventManager.instance.onPurchaseChest = (id) => {
            var data = DataManager.instance.GetShopData(id);
            Debug.LogFormat("{0}, {1}, {2}", data.id, data.name, data.price);
        };

        this.uiShop.Init(); //UI Shop�� �ʱ�ȭ 
    }

}
