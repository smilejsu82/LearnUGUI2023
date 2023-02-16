using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//GameScene�� �ִ� ��� UI�� ���� 
public class UIGame : MonoBehaviour
{
    public TMP_Text txtMessage;
    public UISkillButton uiSkillButton;

    private Coroutine routineShowMessage;   //�⺻�� ? null

    void Start()
    {
        //UIGame �ʱ�ȭ 
        this.txtMessage.gameObject.SetActive(false);

        this.uiSkillButton.onNotAvailable = () => {
            if (this.routineShowMessage != null) {
                //���� �ڷ�ƾ�� ���ư��� �ִٸ� ���� ��Ų�� 
                this.StopCoroutine(this.routineShowMessage);
            }
            //���ο� �ڷ�ƾ�� ������ 
            this.routineShowMessage = this.StartCoroutine(this.ShowMessageRoutine("Not available yet."));
        };

        this.uiSkillButton.Init();    
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        //Ȱ��ȭ�� �ϰ� message�� �����ش� 
        this.txtMessage.gameObject.SetActive(true);
        this.txtMessage.text = message;
        //�����ð��� ������ ��Ȱ��ȭ 

        yield return new WaitForSeconds(1f);
        //��Ȱ��ȭ 
        this.txtMessage.gameObject.SetActive(false);
    }
}
