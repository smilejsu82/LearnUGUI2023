using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIStageGroup : MonoBehaviour
{
    public List<UIStageCellView>cellviewList;

    private int nextPageOpenCellViewId = -1;

    public System.Action onMoveNextPage;
    public System.Action onUpdateStars;

    private void Awake()
    {
        foreach (var cellView in this.cellviewList) {
            cellView.onClick = (id) => {
                Debug.LogFormat("<color=cyan>{0}</color>", id);
                //Ŭ���� �׽�Ʈ 

                var info = InfoManager.instance.GetStageInfo(id);
                info.state = 1; //complete 
                //������ ��Ÿ�� ȹ�� 
                info.starsCount = Random.Range(1, 4);   //Exclusive ���� 

                cellView.UpdateStateState(UIEnums.eStageCellViewState.Complete, info.starsCount);

                //���� ���������� �ִٸ� Open 
                if (cellView.stageNum != DataManager.instance.GetStageDataCount())
                {
                    var nextStageNum = cellView.stageNum + 1;   //���� ���� ++ , -- ������ ���� ���� (�ǿ����ڸ� 1 ���� ��Ŵ)
                    //Debug.LogFormat("nextStageNum: {0}", nextStageNum);

                    //foreach (var view in this.cellviewList) {
                    //    Debug.LogFormat("id: {0}, stageNum: {1}", view.id, view.stageNum);
                    //}

                    var nextCellView = this.cellviewList.Find(x => x.stageNum == nextStageNum);
                    if (nextCellView != null)
                    {
                        
                        var nextCellViewInfo = InfoManager.instance.GetStageInfo(nextCellView.id);
                        nextCellViewInfo.state = 0; //Open 
                                                    //UI������Ʈ 
                        nextCellView.UpdateStateState(UIEnums.eStageCellViewState.Open);
                    }
                    else 
                    {
                        //next�� ���� �������� �ִٴ°��� 
                        //�������� 
                        this.nextPageOpenCellViewId = cellView.id + 1;
                        //�׸��� ���� ��ư�� ������ id���� -1�� �ƴϸ� ó�� 

                        //info ������ �ؾ� �� 
                        var nextStageInfo = InfoManager.instance.StageInfos.Find(x => x.id == this.nextPageOpenCellViewId);
                        nextStageInfo.state = 0;    //Open

                        //(�ڵ����� ������ �ѱ�)
                        this.onMoveNextPage();
                    }
                    
                }

                //���� 
                InfoManager.instance.SaveStageInfos();

                //������ �̺�Ʈ �߼� 
                this.onUpdateStars();

            };
        }
    }

    //public void Init(int begin, int end)
    //{
    //    var curr = (from info in InfoManager.instance.StageInfos
    //             where info.state == 0
    //             let cnt = info.starsCount
    //             select new { curr = cnt }).FirstOrDefault().curr;

    //    var total = InfoManager.instance.StageInfos.Count * 3;

    //    Debug.LogFormat("Init: <color=cyan>{0} / {1}</color>", curr, total);

    //    this.onUpdateStars(curr, total);

    //    this.UpdateCellViews(begin, end);
    //}

    //1, 18 
    //19, 36 
    public void UpdateCellViews(int begin, int end)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int i = 0;

        for (int stageNum = begin; stageNum <= end; stageNum++) {
            sb.Append(stageNum);
            sb.Append(" ");

            this.cellviewList[i].UpdateStageNum(stageNum);

            i++;
        }
        //1, 2, 3, 4, 5, 6... 18 
        //19, 20, 21, ... 36 
        Debug.Log(sb.ToString());

        //��⿡�� .... ĳ�� �Ǿ� �ִ� ���� Open �� cellview�� ������Ʈ 
        if (this.nextPageOpenCellViewId != -1)
        {
            Debug.LogFormat("ĳ�� �Ǿ� �ִ� next open cellview ID : {0}", this.nextPageOpenCellViewId);

            //UI ������Ʈ �ϰ� 
            //���� ���������� ã����� �ϸ� �ȴ� ���� �������� �Ѿ�� ã�ƾ� �� 
            var cellview = this.cellviewList.Find(x => x.id == this.nextPageOpenCellViewId);
            cellview.UpdateStateState(UIEnums.eStageCellViewState.Open);

            //ĳ�� ���� 
            this.nextPageOpenCellViewId = -1;

        }
    }
}
