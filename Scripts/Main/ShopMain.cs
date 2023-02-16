using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMain : MonoBehaviour
{
    
    public UIShop uiShop;

    void Start()
    {
        DataManager.instance.LoadShopData();    //데이터 로드 

        EventManager.instance.onBtnAdClick = () => {
            Debug.Log("광고를 보여줍니다.");
        };

        EventManager.instance.onPurchaseChest = (id) => {
            var data = DataManager.instance.GetShopData(id);
            Debug.LogFormat("{0}, {1}, {2}", data.id, data.name, data.price);
        };

        this.uiShop.Init(); //UI Shop을 초기화 
    }

}
