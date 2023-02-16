using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMissionScrollView : MonoBehaviour
{
    public GameObject cellviewPrefab;
    public Transform contentTrans;

    public void Init()
    {
        Debug.LogFormat("<color=yellow>[UIMissionScrollView] Init()</color>");
        //test
        foreach (var data in DataManager.instance.GetDataDic<MissionData>().Values) {
            var go = Instantiate(this.cellviewPrefab, this.contentTrans);
            var cellview = go.GetComponent<UIMissionCellView>();

            var info = InfoManager.instance.GetMissionInfo(data.id);
            cellview.btn.onClick.AddListener(() => {
                Debug.LogFormat("selected mission -> id: {0}", info.id);
                //test 

                var state = (UIEnums.eMissionState)info.state;

                if (state == UIEnums.eMissionState.Doing)
                {

                    info.progress += 1;

                    //ui update 
                    cellview.slider.value = (float)info.progress / data.goal_val;
                    cellview.txtProgress.text = string.Format("{0} / {1}", info.progress, data.goal_val);

                    if (info.progress == data.goal_val)
                    {
                        //done 
                        info.state = (int)UIEnums.eMissionState.Done;
                        //change state 
                        cellview.SetState(UIEnums.eMissionState.Done);
                    }

                    //save 
                    InfoManager.instance.SaveMissionInfos();
                }
                else {
                    Debug.LogFormat("<color=yellow>state: {0}</color>", state);
                }

            });

            cellview.Init(info);
        }
    }
}
