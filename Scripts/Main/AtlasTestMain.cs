using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class AtlasTestMain : MonoBehaviour
{
    public Button[] arrBtns;
    public Image imgChest;  //Ÿ�� �̹��� 

    public Texture2D[] srcTextures; //�ҽ� �̹���

    public SpriteAtlas atlas;   //��Ʋ�� 

    void Start()
    {
        for (int i = 0; i < this.arrBtns.Length; i++) {
            var btn =  this.arrBtns[i];
            int idx = i;    //���� ĸ�� 
            btn.onClick.AddListener(() => {
                //Ŭ����(����)
                //Debug.LogFormat("i: {0}, idx: {1}", i, idx);

                Texture2D texture = this.srcTextures[idx];  //0, 1

                var spriteName = string.Format("shop_img_chest_close_m_0{0}", idx);
                Debug.Log(spriteName);

                var sprite = this.atlas.GetSprite(spriteName);
                //Sprite �� ���� 
                //Sprite sprite = 
                //    Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                this.imgChest.sprite = sprite;
                this.imgChest.SetNativeSize();
                
            });
        }  
    }
}
