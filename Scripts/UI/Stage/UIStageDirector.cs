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
        //���¿� ��� ���� info�� ��� star ���� 
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

        //next�Ǵ� prev��ư�� Ȱ��ȭ �Ǵ� ��Ȱ�� 
        if (this.currentPageNum == 1)   //ù������ 
            this.btnPrev.gameObject.SetActive(false);
        else
            this.btnPrev.gameObject.SetActive(true);

        if (this.currentPageNum == this.totalPages)   //������ ������  
            this.btnNext.gameObject.SetActive(false);
        else
            this.btnNext.gameObject.SetActive(true);
    }

    public void Init()
    {
        //���� �������� �ش��ϴ� begin ~ end ���� StageCellView�� �����ش� 

        //Ceil(18 / MAX_STAGE) --> 1
        //Ceil(19 / MAX_STAGE)  --> 2 

        this.totalPages = Mathf.CeilToInt((float)DataManager.instance.GetStageDataCount() / MAX_STAGE);

        //���� �ȱ� ���� �ϳ��� �ִٸ� 
        if (InfoManager.instance.StageInfos.Any(x => x.state == 0))
        {
            //currentPageNum�� ���� �ֱ� Open�� StageNum�� ������� �ؾ� �� 
            var idx = InfoManager.instance.StageInfos.FindIndex(x => x.state == 0);
            Debug.LogFormat("<color=cyan>idx: {0}</color>", idx);
            //idx : Open�� ���������� �ε��� 
            var stageNum = idx + 1;
            Debug.LogFormat("stageNum: {0}", stageNum); //19 
            this.currentPageNum = Mathf.CeilToInt((float)stageNum / MAX_STAGE);    //19/18          //������ ������ ����

            Debug.LogFormat("<color=yellow>currentPageNum: {0} </color>", currentPageNum);
        }
        else
        {
            //��δ� Ŭ���� �ߴٸ� ���� ������ �������� 
            this.currentPageNum = this.totalPages;
        }

        this.UpdatePage();

        this.UpdateStarsCountUI();  //��Ÿ ������Ʈ 
    }

}
