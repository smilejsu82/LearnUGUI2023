using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class UIRewardDailyCellView : MonoBehaviour
{
    public Image imgFrame;

    public TMP_Text txtDay;
    public Image imgIcon;
    public TMP_Text txtAmount;

    public GameObject checkGo;
    public GameObject focusGo;

    public int id;
    private SpriteAtlas atlas;

    public void Init(int id, int day, int amount, Sprite sp, int state)
    {
        this.id = id;
        this.txtDay.text = day.ToString();
        this.txtAmount.text = amount.ToString();
        this.imgIcon.sprite = sp;
        this.imgIcon.SetNativeSize();
        this.atlas = AtlasManager.instance.GetAtlasByName("UIRewardDaily");

        if (state == 0) //아직 안먹었다 
        {
            this.ResetUI();
        }
        else 
        {
            //먹었다 
            this.Claim();
        }

    }

    public void Claim()
    {
        this.checkGo.SetActive(true);
        //배경 바꾸기 
        var sp = atlas.GetSprite("frame_stageframe_01_d");
        this.imgFrame.sprite = sp;
        //포커스 되어 있다면 포커 끄기 
        this.focusGo.SetActive(false);
    }

    public void Focus()
    {
        var sp = atlas.GetSprite("frame_stageframe_01_s1");
        this.imgFrame.sprite = sp;
        this.focusGo.SetActive(true);
    }

    public void ResetUI()
    {
        var sp = atlas.GetSprite("frame_stageframe_01_n");
        this.imgFrame.sprite = sp;
        this.focusGo.SetActive(false);
        this.checkGo.SetActive(false);
    }
}
