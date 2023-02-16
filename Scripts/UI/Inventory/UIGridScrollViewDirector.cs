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
            //������ info : null
            if (foundInfo == null)
            {
                //�κ��丮�� ���� 
                ItemInfo info = new ItemInfo(id);
                InfoManager.instance.InventoryInfo.itemInfos.Add(info); //���� ������ ȹ�� 
            }
            else 
            {
                foundInfo.amount++;
            }

            //���� 
            InfoManager.instance.SaveInventoryInfo();

            this.scrollview.Refresh();

        });

        this.scrollview.Init();
    }


    private void SellItem(int id)
    {
        var info = InfoManager.instance.InventoryInfo.itemInfos.Find(x => x.id == id);

        Debug.LogFormat("�ȷ��� �ϴ� ������ id : {0}, amount: {1}", info.id, info.amount);   //118, 3

        if (info.amount > 1)
        {
            --info.amount;
        }
        else 
        {
            //����� 
            InfoManager.instance.InventoryInfo.itemInfos.Remove(info);

            foreach (var x in InfoManager.instance.InventoryInfo.itemInfos)
            {
                Debug.LogFormat("--------> x: {0} , amount: {1}", x.id, x.amount);
            }

            //close 
            this.popupDetail.Close();
        }

        //���� 
        InfoManager.instance.SaveInventoryInfo();

        //����Ʈ ���� ��ħ 
        this.scrollview.Refresh();



    }
}
