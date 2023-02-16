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

    //초기화 
    public void Init() {

        Debug.Log("init");

        //cooltime 이미지의 fillAmount = 0
        this.imgCoolTime.fillAmount = 0;
        //txtCoolTime 비활성화 
        this.txtCoolTime.gameObject.SetActive(false);
    }

    private void UseSkill()
    {
        if (this.isCoolTime) {
            //이벤트 전송 
            this.onNotAvailable();
            return;
        }

        this.isCoolTime = true;

        //cooltime 이미지의 fillAmount = 1
        this.imgCoolTime.fillAmount = 1;
        //txtCoolTime 활성화 
        this.txtCoolTime.gameObject.SetActive(true);
        //쿨타임을 보여준다 
        this.txtCoolTime.text = string.Format("{0}", this.coolTime);

        //시간재기 (Update, 코루틴)
        this.StartCoroutine(this.WaitForCoolTime());
    }

    private IEnumerator WaitForCoolTime()
    {
        float delta = this.coolTime;

        while (true) {
            delta -= Time.deltaTime;
            //Debug.Log(delta);
            //UI에 Text에 업데이트 
            this.txtCoolTime.text = string.Format("{0}", (int)delta);

            //imgCoolTime의 fillAmount도 같이 갱신 (fillAmount의 값은 0 ~ 1까지)
            float fillAmount = delta / this.coolTime;
            Debug.Log(fillAmount);
            this.imgCoolTime.fillAmount = fillAmount;

            if (delta <= 0) {
                break;
            }
            yield return null;
        }

        Debug.Log("스킬 사용가능");
        //txtCoolTime 비활성화 
        this.txtCoolTime.gameObject.SetActive(false);

        //flag 변경 
        this.isCoolTime = false;

    }
}
