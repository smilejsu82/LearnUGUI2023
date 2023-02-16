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
    public TMP_Text txtName;     //�޴� �� 
    public GameObject focusGo;  //��Ŀ�� �̹��� 
    
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
        //���� �� ���� 
        //F4DE9E 
        string htmlString = "#F4DE9E";
        Color color;
        if (ColorUtility.TryParseHtmlString(htmlString, out color)) {
            this.txtName.color = color;
        }

        //��Ŀ�� �̹��� Ȱ��ȭ 
        this.focusGo.SetActive(true);
    }

    public void FocusOff()
    {
        //���� �� ���� 
        //FFFFFF 
        string htmlString = "#FFFFFF";
        Color color;
        if (ColorUtility.TryParseHtmlString(htmlString, out color))
        {
            this.txtName.color = color;
        }

        //��Ŀ�� �̹��� ��Ȱ��ȭ 
        this.focusGo.SetActive(false);
    }
}
