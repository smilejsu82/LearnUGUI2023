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
                //클리어 테스트 

                var info = InfoManager.instance.GetStageInfo(id);
                info.state = 1; //complete 
                //랜덤한 스타를 획득 
                info.starsCount = Random.Range(1, 4);   //Exclusive 주의 

                cellView.UpdateStateState(UIEnums.eStageCellViewState.Complete, info.starsCount);

                //다음 스테이지가 있다면 Open 
                if (cellView.stageNum != DataManager.instance.GetStageDataCount())
                {
                    var nextStageNum = cellView.stageNum + 1;   //증가 감소 ++ , -- 연산자 사용시 주의 (피연산자를 1 증가 시킴)
                    //Debug.LogFormat("nextStageNum: {0}", nextStageNum);

                    //foreach (var view in this.cellviewList) {
                    //    Debug.LogFormat("id: {0}, stageNum: {1}", view.id, view.stageNum);
                    //}

                    var nextCellView = this.cellviewList.Find(x => x.stageNum == nextStageNum);
                    if (nextCellView != null)
                    {
                        
                        var nextCellViewInfo = InfoManager.instance.GetStageInfo(nextCellView.id);
                        nextCellViewInfo.state = 0; //Open 
                                                    //UI업데이트 
                        nextCellView.UpdateStateState(UIEnums.eStageCellViewState.Open);
                    }
                    else 
                    {
                        //next가 다음 페이지에 있다는거임 
                        //저장하자 
                        this.nextPageOpenCellViewId = cellView.id + 1;
                        //그리고 다음 버튼을 눌러서 id값이 -1이 아니면 처리 

                        //info 저장은 해야 함 
                        var nextStageInfo = InfoManager.instance.StageInfos.Find(x => x.id == this.nextPageOpenCellViewId);
                        nextStageInfo.state = 0;    //Open

                        //(자동으로 페이지 넘김)
                        this.onMoveNextPage();
                    }
                    
                }

                //저장 
                InfoManager.instance.SaveStageInfos();

                //저장후 이벤트 발송 
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

        //요기에서 .... 캐싱 되어 있는 다음 Open 된 cellview를 업데이트 
        if (this.nextPageOpenCellViewId != -1)
        {
            Debug.LogFormat("캐싱 되어 있는 next open cellview ID : {0}", this.nextPageOpenCellViewId);

            //UI 업데이트 하고 
            //현재 페이지에서 찾을라고 하면 안댐 다음 페이지로 넘어가서 찾아야 함 
            var cellview = this.cellviewList.Find(x => x.id == this.nextPageOpenCellViewId);
            cellview.UpdateStateState(UIEnums.eStageCellViewState.Open);

            //캐싱 비우기 
            this.nextPageOpenCellViewId = -1;

        }
    }
}
