using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class AtlasTestMain : MonoBehaviour
{
    public Button[] arrBtns;
    public Image imgChest;  //타겟 이미지 

    public Texture2D[] srcTextures; //소스 이미지

    public SpriteAtlas atlas;   //아틀라스 

    void Start()
    {
        for (int i = 0; i < this.arrBtns.Length; i++) {
            var btn =  this.arrBtns[i];
            int idx = i;    //변수 캡쳐 
            btn.onClick.AddListener(() => {
                //클로져(람다)
                //Debug.LogFormat("i: {0}, idx: {1}", i, idx);

                Texture2D texture = this.srcTextures[idx];  //0, 1

                var spriteName = string.Format("shop_img_chest_close_m_0{0}", idx);
                Debug.Log(spriteName);

                var sprite = this.atlas.GetSprite(spriteName);
                //Sprite 를 생성 
                //Sprite sprite = 
                //    Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                this.imgChest.sprite = sprite;
                this.imgChest.SetNativeSize();
                
            });
        }  
    }
}
