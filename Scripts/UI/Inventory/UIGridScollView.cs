using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridScollView : MonoBehaviour
{
    public Transform content;
    public GameObject cellviewPrefab;
    public GameObject txtNoItemsGo;

    private const int THRESHOLD = 18;

    private UIGridCellView currentFocusCellView;

    public System.Action<int> onFocus;

    public void Init()
    {
        this.txtNoItemsGo.SetActive(InfoManager.instance.InventoryInfo.itemInfos.Count == 0);

       this.CreateCellViews();

        this.GetComponent<ScrollRect>().vertical = InfoManager.instance.InventoryInfo.itemInfos.Count > THRESHOLD;
    }

    private Coroutine routine;

    public void Refresh()
    {

        //새로운 데이터 로드 
        InfoManager.instance.LoadInventoryInfo();

        foreach (Transform child in this.content)
            Destroy(child.gameObject);

        //다시 붙임 
        this.CreateCellViews();

        this.txtNoItemsGo.SetActive(InfoManager.instance.InventoryInfo.itemInfos.Count == 0);

        var rect = this.GetComponent<ScrollRect>();
        rect.verticalNormalizedPosition = 1f;   //(1=top, 0=bottom)

        rect.vertical = InfoManager.instance.InventoryInfo.itemInfos.Count > THRESHOLD;
    }

    private void CreateCellViews()
    {
        for (int i = 0; i < InfoManager.instance.InventoryInfo.itemInfos.Count; i++)
        {
            //cellview prefab의 인스턴스 (clone)
            var go = Instantiate(this.cellviewPrefab, this.content);
            var cellview = go.GetComponent<UIGridCellView>();
            var btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                Debug.Log(cellview.id);
                if (this.currentFocusCellView != null) {
                    //선택 되어 있는 CellView가 있다 
                    this.currentFocusCellView.Focus(false);
                }

                cellview.Focus(true);
                this.currentFocusCellView = cellview;

                //이벤트 전송 
                this.onFocus(this.currentFocusCellView.id);
            });

            
            //id, 아이콘, 수량
            var info = InfoManager.instance.InventoryInfo.itemInfos[i];
            var data = DataManager.instance.GetItemData(info.id);
            var atlas = AtlasManager.instance.GetAtlasByName("UIItemIcon");
            var sprite = atlas.GetSprite(data.sprite_name);
            var amount = info.amount;
            cellview.Init(info.id, sprite, amount);
        }
    }
}
