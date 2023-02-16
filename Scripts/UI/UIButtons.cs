using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//모든 UI를 관리 
//GameMain,  UIGame
//LobbyMain, UILobby 
//TileMain, UITitle 
//StageMain, UIStage
public class UIButtons : MonoBehaviour
{
    //public UITabMenu uiTabMenu;
    //public UISlider uiSlider;
    public UIInputText uiInputText;
    public Button btn;

    public UIPopupLogin uiPopupLogin;

    public void Init()
    {
        this.uiPopupLogin.btnClose.onClick.AddListener(() => {
            this.uiPopupLogin.Close();
        });

        this.btn.onClick.AddListener(() => {

            //open popup
            this.uiPopupLogin.Open();

        });

        //this.uiTabMenu.Init();
        //this.uiSlider.onSliderValueChanged = (val) => {
        //    Debug.LogFormat("UIButtons: {0}", val);
        //};

        //this.uiSlider.Init(0.5f);
    }
}
