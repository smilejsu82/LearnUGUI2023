using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIPopupLogin : MonoBehaviour
{
    public Button btnClose;
    public TMP_InputField inputId;
    public TMP_InputField inputPassword;
    public Button btnLogin;
    public Button btnSignup;
    public Button btnForgotPw;
    public Toggle toggleRemember;

    public System.Action<string, string> onClickLogin;

    private void Awake()
    {
        Debug.LogFormat("isOn: {0}", this.toggleRemember.isOn);

        this.toggleRemember.onValueChanged.AddListener((val) => {
            Debug.LogFormat("isOn: {0}", val);
        });

        this.btnSignup.onClick.AddListener(() => {
            Debug.Log("Sign up");
        });
        this.btnForgotPw.onClick.AddListener(() => {
            Debug.Log("Forgot Password");
        });

        this.btnLogin.onClick.AddListener(() => {
            string id = this.inputId.text;
            string password = this.inputPassword.text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
            {
                Debug.LogFormat("<color=cyan>ID 또는 Password를 채워야 합니다.</color>");
            }
            else 
            {
                Debug.LogFormat("id: {0}", id);
                Debug.LogFormat("password: {0}", password);

                this.onClickLogin(id, password);
            }
            
        });
    }

    public void Init()
    { 

    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

}
