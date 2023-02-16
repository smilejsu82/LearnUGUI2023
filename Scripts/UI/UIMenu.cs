using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenu : MonoBehaviour
{
    public enum eMenuType { 
        None = -1, 
        Shop, Item
    }

    public Button btn;
    public TMP_Text txtName;     //메뉴 명 
    public GameObject focusGo;  //포커스 이미지 
    
    public eMenuType menuType;
    public System.Action<eMenuType> onClick;

    private void Start()
    {
        //this.btn.onClick.AddListener(() => {
        //    this.onClick(this.menuType);
        //});
    }

    public void FocusOn()
    {
        //글자 색 변경 
        //F4DE9E 
        string htmlString = "#F4DE9E";
        Color color;
        if (ColorUtility.TryParseHtmlString(htmlString, out color)) {
            this.txtName.color = color;
        }

        //포커스 이미지 활성화 
        this.focusGo.SetActive(true);
    }

    public void FocusOff()
    {
        //글자 색 변경 
        //FFFFFF 
        string htmlString = "#FFFFFF";
        Color color;
        if (ColorUtility.TryParseHtmlString(htmlString, out color))
        {
            this.txtName.color = color;
        }

        //포커스 이미지 비활성화 
        this.focusGo.SetActive(false);
    }
}
