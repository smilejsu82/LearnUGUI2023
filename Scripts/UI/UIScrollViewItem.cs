using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScrollViewItem : MonoBehaviour
{
    public TMP_Text txtName;
    public Image imgChest;
    public Button btnGem;
    public TMP_Text txtPrice;
    public int id;

    public void Init(ShopData data) {
        
        this.id = data.id;

        this.txtName.text = data.name;

        var atlas = AtlasManager.instance.GetAtlasByName("UIShop");
        this.imgChest.sprite = atlas.GetSprite(data.sprite_name);
        this.imgChest.SetNativeSize();

        if (data.type == 1) //AD
        {
            //300, 277
            this.imgChest.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 277);
        }

        this.txtPrice.text = string.Format("{0:#,0}", data.price);
    }
}
