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

        //���ο� ������ �ε� 
        InfoManager.instance.LoadInventoryInfo();

        foreach (Transform child in this.content)
            Destroy(child.gameObject);

        //�ٽ� ���� 
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
            //cellview prefab�� �ν��Ͻ� (clone)
            var go = Instantiate(this.cellviewPrefab, this.content);
            var cellview = go.GetComponent<UIGridCellView>();
            var btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                Debug.Log(cellview.id);
                if (this.currentFocusCellView != null) {
                    //���� �Ǿ� �ִ� CellView�� �ִ� 
                    this.currentFocusCellView.Focus(false);
                }

                cellview.Focus(true);
                this.currentFocusCellView = cellview;

                //�̺�Ʈ ���� 
                this.onFocus(this.currentFocusCellView.id);
            });

            
            //id, ������, ����
            var info = InfoManager.instance.InventoryInfo.itemInfos[i];
            var data = DataManager.instance.GetItemData(info.id);
            var atlas = AtlasManager.instance.GetAtlasByName("UIItemIcon");
            var sprite = atlas.GetSprite(data.sprite_name);
            var amount = info.amount;
            cellview.Init(info.id, sprite, amount);
        }
    }
}
