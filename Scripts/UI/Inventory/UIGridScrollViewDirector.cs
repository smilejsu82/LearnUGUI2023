using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridScrollViewDirector : MonoBehaviour
{
    public UIGridScollView scrollview;
    public Button btnTestGetItem;

    public UIPopupItemDetail popupDetail;

    public void Init()
    {
        this.popupDetail.onSell = (id) => {
            Debug.LogFormat("<color=magenta>Sell Item : {0}</color>", id);

            this.SellItem(id);
        };

        this.scrollview.onFocus = (id) => {

            var foundInfo = InfoManager.instance.InventoryInfo.itemInfos.Find(x => x.id == id);

            Debug.LogFormat("<color=yellow>[UIGridScrollViewDirector] onFocus : \t id :{0}, amount: {1}</color>", id, foundInfo.amount);

            this.popupDetail.Init(id).Open();
        };

        this.popupDetail.btnClose.onClick.AddListener(() => {
            this.popupDetail.Close();
        });

        this.btnTestGetItem.onClick.AddListener(() => {

            var data = DataManager.instance.GetRandomItemData();

            var id = data.id;
            var foundInfo = InfoManager.instance.InventoryInfo.itemInfos.Find(x => x.id == id);
            //없으면 info : null
            if (foundInfo == null)
            {
                //인벤토리에 없다 
                ItemInfo info = new ItemInfo(id);
                InfoManager.instance.InventoryInfo.itemInfos.Add(info); //최초 아이템 획득 
            }
            else 
            {
                foundInfo.amount++;
            }

            //저장 
            InfoManager.instance.SaveInventoryInfo();

            this.scrollview.Refresh();

        });

        this.scrollview.Init();
    }


    private void SellItem(int id)
    {
        var info = InfoManager.instance.InventoryInfo.itemInfos.Find(x => x.id == id);

        Debug.LogFormat("팔려고 하는 아이템 id : {0}, amount: {1}", info.id, info.amount);   //118, 3

        if (info.amount > 1)
        {
            --info.amount;
        }
        else 
        {
            //지우고 
            InfoManager.instance.InventoryInfo.itemInfos.Remove(info);

            foreach (var x in InfoManager.instance.InventoryInfo.itemInfos)
            {
                Debug.LogFormat("--------> x: {0} , amount: {1}", x.id, x.amount);
            }

            //close 
            this.popupDetail.Close();
        }

        //저장 
        InfoManager.instance.SaveInventoryInfo();

        //리스트 새로 고침 
        this.scrollview.Refresh();



    }
}
