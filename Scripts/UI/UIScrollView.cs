using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollView : MonoBehaviour
{
    public Transform contentTrans;
    public GameObject itemADPrefab;
    public GameObject itemPrefab;

    public void Init()
    {

        List<ShopData> list =  DataManager.instance.GetShopDatas();

        foreach (ShopData data in list) {
            if (data.type == 0)
            {
                //normal
                this.AddItem(data);
            }
            else if (data.type == 1) {
                //ad
                this.AddItemAD(data);
            }
        }
    }

    public void AddItemAD(ShopData data)
    {
        var go = Instantiate(this.itemADPrefab, this.contentTrans);
        UIScrollViewItemAD itemAD = go.GetComponent<UIScrollViewItemAD>();

        itemAD.Init(data);

        itemAD.btnAD.onClick.AddListener(() => {
            Debug.LogFormat("[show ad] id: {0}", itemAD.id);

            //사건의 발생 
            EventManager.instance.onBtnAdClick();

        });
        itemAD.btnGem.onClick.AddListener(() => {
            Debug.LogFormat("[AD] purchase chest : id : {0}", itemAD.id);

            EventManager.instance.onPurchaseChest(itemAD.id);

        });
    }

    public void AddItem(ShopData data)
    {
        var go = Instantiate(this.itemPrefab, this.contentTrans);
        UIScrollViewItem item = go.GetComponent<UIScrollViewItem>();

        item.Init(data);

        item.btnGem.onClick.AddListener(() => {
            Debug.LogFormat("purchase chest : id: {0}", item.id);

            EventManager.instance.onPurchaseChest(item.id);
        });
    }
}
