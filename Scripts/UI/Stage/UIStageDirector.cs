using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIStageDirector : MonoBehaviour
{
    public Button btnPrev;
    public Button btnNext;
    public UIStageGroup group;
    public TMP_Text txtPage;
    public TMP_Text txtStars;


    private const int MAX_STAGE = 18;
    private int currentPageNum = 1;
    private int totalPages;

    private void Awake()
    {
        this.btnPrev.onClick.AddListener(() =>
        {
            this.MovePrevPage();
        });

        this.btnNext.onClick.AddListener(() =>
        {
            this.MoveNextPage();
        });

        this.group.onMoveNextPage = () =>
        {
            this.MoveNextPage();
        };

        this.group.onUpdateStars = () =>
        {
            this.UpdateStarsCountUI();
        };
    }

    private void UpdateStarsCountUI()
    {
        //상태와 상관 없이 info의 모든 star 개수 
        var curr = 0;
        foreach (var info in InfoManager.instance.StageInfos) {
            curr += info.starsCount;
        }

        var total = InfoManager.instance.StageInfos.Count * 3;

        Debug.LogFormat("<color=cyan>{0} / {1}</color>", curr, total);

        this.txtStars.text = string.Format("{0} / {1}", curr, total);
    }

    private void MovePrevPage()
    {
        this.currentPageNum--;
        this.UpdatePage();
    }

    private void MoveNextPage()
    {
        this.currentPageNum++;
        this.UpdatePage();
    }

    private void UpdatePage()
    {
        //Debug.Log(this.currentPageNum);

        this.txtPage.text = string.Format("{0}/{1}", this.currentPageNum, this.totalPages);

        Debug.LogFormat("<color=yellow>{0}/{1}</color>", this.currentPageNum, this.totalPages);

        // end : page_num * MAX_STAGE               1 * 18 = 18 ,      2 * 18 = 36
        // begin : end - MAX_STAGE + 1              18 - 18 + 1 = 1,     36 -18 + 1 = 19





        var end = this.currentPageNum * MAX_STAGE;
        var begin = end - MAX_STAGE + 1;

        this.group.UpdateCellViews(begin, end);

        //next또는 prev버튼을 활성화 또는 비활성 
        if (this.currentPageNum == 1)   //첫페이지 
            this.btnPrev.gameObject.SetActive(false);
        else
            this.btnPrev.gameObject.SetActive(true);

        if (this.currentPageNum == this.totalPages)   //마지막 페이지  
            this.btnNext.gameObject.SetActive(false);
        else
            this.btnNext.gameObject.SetActive(true);
    }

    public void Init()
    {
        //현재 페이지에 해당하는 begin ~ end 까지 StageCellView를 보여준다 

        //Ceil(18 / MAX_STAGE) --> 1
        //Ceil(19 / MAX_STAGE)  --> 2 

        this.totalPages = Mathf.CeilToInt((float)DataManager.instance.GetStageDataCount() / MAX_STAGE);

        //아직 안깬 판이 하나라도 있다면 
        if (InfoManager.instance.StageInfos.Any(x => x.state == 0))
        {
            //currentPageNum가 가장 최근 Open된 StageNum을 기반으로 해야 함 
            var idx = InfoManager.instance.StageInfos.FindIndex(x => x.state == 0);
            Debug.LogFormat("<color=cyan>idx: {0}</color>", idx);
            //idx : Open된 스테이지의 인덱스 
            var stageNum = idx + 1;
            Debug.LogFormat("stageNum: {0}", stageNum); //19 
            this.currentPageNum = Mathf.CeilToInt((float)stageNum / MAX_STAGE);    //19/18          //정수형 나누기 주의

            Debug.LogFormat("<color=yellow>currentPageNum: {0} </color>", currentPageNum);
        }
        else
        {
            //모두다 클리어 했다면 가장 마지막 페이지로 
            this.currentPageNum = this.totalPages;
        }

        this.UpdatePage();

        this.UpdateStarsCountUI();  //스타 업데이트 
    }

}
