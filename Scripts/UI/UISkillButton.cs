using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UISkillButton : MonoBehaviour
{
    private Button btn;
    private bool isCoolTime;
    private int coolTime = 5;

    public Image imgCoolTime;
    public TMP_Text txtCoolTime;
    public System.Action onNotAvailable;

    void Start()
    {
        Debug.Log("start");

        this.btn = this.GetComponent<Button>();

        this.btn.onClick.AddListener(() => {
            Debug.Log("click");
            this.UseSkill();
        });    
    }

    //�ʱ�ȭ 
    public void Init() {

        Debug.Log("init");

        //cooltime �̹����� fillAmount = 0
        this.imgCoolTime.fillAmount = 0;
        //txtCoolTime ��Ȱ��ȭ 
        this.txtCoolTime.gameObject.SetActive(false);
    }

    private void UseSkill()
    {
        if (this.isCoolTime) {
            //�̺�Ʈ ���� 
            this.onNotAvailable();
            return;
        }

        this.isCoolTime = true;

        //cooltime �̹����� fillAmount = 1
        this.imgCoolTime.fillAmount = 1;
        //txtCoolTime Ȱ��ȭ 
        this.txtCoolTime.gameObject.SetActive(true);
        //��Ÿ���� �����ش� 
        this.txtCoolTime.text = string.Format("{0}", this.coolTime);

        //�ð���� (Update, �ڷ�ƾ)
        this.StartCoroutine(this.WaitForCoolTime());
    }

    private IEnumerator WaitForCoolTime()
    {
        float delta = this.coolTime;

        while (true) {
            delta -= Time.deltaTime;
            //Debug.Log(delta);
            //UI�� Text�� ������Ʈ 
            this.txtCoolTime.text = string.Format("{0}", (int)delta);

            //imgCoolTime�� fillAmount�� ���� ���� (fillAmount�� ���� 0 ~ 1����)
            float fillAmount = delta / this.coolTime;
            Debug.Log(fillAmount);
            this.imgCoolTime.fillAmount = fillAmount;

            if (delta <= 0) {
                break;
            }
            yield return null;
        }

        Debug.Log("��ų ��밡��");
        //txtCoolTime ��Ȱ��ȭ 
        this.txtCoolTime.gameObject.SetActive(false);

        //flag ���� 
        this.isCoolTime = false;

    }
}
