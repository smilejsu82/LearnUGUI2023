using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIShop : MonoBehaviour
{
    public UIScrollView uiScrollView;
    public SpriteAtlas atlas;

    public void Init()
    {
        this.uiScrollView.Init();
    }

}
