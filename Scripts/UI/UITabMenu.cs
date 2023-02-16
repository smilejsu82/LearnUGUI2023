using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabMenu : MonoBehaviour
{
    public UIMenu[] arrUIMenu;
    private UIMenu selectedUIMenu;
    private UIMenu.eMenuType menuType = UIMenu.eMenuType.None; //선택된 메뉴 타입 

    void Start()
    {
        foreach (UIMenu uiMenu in this.arrUIMenu) {
            uiMenu.btn.onClick.AddListener(() =>
            {
                this.SelectMenu(uiMenu.menuType);
            });
        }
    }

    public void Init()
    {
        this.SelectMenu(UIMenu.eMenuType.Shop);
    }

    public void SelectMenu(UIMenu.eMenuType menuType)
    {
        Debug.LogFormat("prev: {0}, current: {1}", this.menuType, menuType);

        if (this.menuType != menuType)
        {
            this.menuType = menuType;

            if (this.selectedUIMenu != null) {
                this.selectedUIMenu.FocusOff();
            }
            var uiMenu = this.arrUIMenu[(int)this.menuType];
            uiMenu.FocusOn();
            this.selectedUIMenu = uiMenu;
        }
    }
}
