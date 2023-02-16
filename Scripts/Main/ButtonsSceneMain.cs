using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//¾À ¾È¿¡ ÀÖ´Â ¸ðµç »óÈ²À» ÃÑ°ý 
public class ButtonsSceneMain : MonoBehaviour
{
    public UIButtons uiButtons; //ui¸¦ ÃÑ°ý 
    
    void Start()
    {
        this.uiButtons.uiPopupLogin.onClickLogin = (id, password) => {

            //https://docs.unity3d.com/kr/560/Manual/StyledText.html
            Debug.LogFormat("<color=yellow>id: {0}, password: {1}</color>", id, password);
        };

        this.uiButtons.Init();
    }
}
