using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStageCellView : MonoBehaviour
{
    public TMP_Text[] arrTxtStageNum;
    public GameObject[] arrStarsGo;
    public GameObject[] arrStateGo;
    public Button btn;
    public int id;
    public int stageNum;

    public System.Action<int> onClick;

    private void Awake()
    {
        this.btn.onClick.AddListener(() => {

            this.onClick(this.id);

        });
    }

    public void UpdateStageNum(int stageNum)
    {
        this.stageNum = stageNum;

        this.id = DataManager.instance.GetStageId(stageNum);

        foreach (var tmpTxt in this.arrTxtStageNum)
        {
            tmpTxt.text = stageNum.ToString();
        }

        //업데이트 상태 info 
        if (id != -1)
        {
            var info = InfoManager.instance.StageInfos.Find(x => x.id == this.id);
            this.UpdateStateState((UIEnums.eStageCellViewState)info.state, info.starsCount);    //별 업데이트 
            this.gameObject.SetActive(true);
        }
        else {
            //업다면 
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateStateState(UIEnums.eStageCellViewState state, int starsCount = 0)
    {
        foreach (var go in this.arrStateGo) go.SetActive(false);

        //0 : Open
        //1 : Complete
        //2 : Lock 
        var idx = (int)state;
        this.arrStateGo[idx].SetActive(true);

        //별 업데이트 
        if (state == UIEnums.eStageCellViewState.Complete) {
            foreach (var go in this.arrStarsGo) go.SetActive(false);

            for (int i = 0; i < starsCount; i++) {
                this.arrStarsGo[i].SetActive(true);
            }
        }
    }
}
