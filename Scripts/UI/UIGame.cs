using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//GameScene에 있는 모든 UI를 관리 
public class UIGame : MonoBehaviour
{
    public TMP_Text txtMessage;
    public UISkillButton uiSkillButton;

    private Coroutine routineShowMessage;   //기본값 ? null

    void Start()
    {
        //UIGame 초기화 
        this.txtMessage.gameObject.SetActive(false);

        this.uiSkillButton.onNotAvailable = () => {
            if (this.routineShowMessage != null) {
                //만약 코루틴이 돌아가고 있다면 중지 시킨다 
                this.StopCoroutine(this.routineShowMessage);
            }
            //새로운 코루틴을 돌린다 
            this.routineShowMessage = this.StartCoroutine(this.ShowMessageRoutine("Not available yet."));
        };

        this.uiSkillButton.Init();    
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        //활성화를 하고 message를 보여준다 
        this.txtMessage.gameObject.SetActive(true);
        this.txtMessage.text = message;
        //일정시간이 지나면 비활성화 

        yield return new WaitForSeconds(1f);
        //비활성화 
        this.txtMessage.gameObject.SetActive(false);
    }
}
