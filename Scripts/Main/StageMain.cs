using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageMain : MonoBehaviour
{
    public UIStageDirector director;

    void Start()
    {
        DataManager.instance.LoadStageData();
        InfoManager.instance.Init();

        var path = string.Format("{0}/stage_infos.json", Application.persistentDataPath);
        Debug.Log(path);

        //신규유저 , 기존유저 판별 
        //* 리스트에 넣고 : InfoManager.instance.stageInfos에 추가 
        if (System.IO.File.Exists(path))
        {
            Debug.Log("기존 유저");
            //기존유저 
            //JSON로드 해서 역직렬화 하고 리스트에 넣고 
            InfoManager.instance.LoadStageInfos();

        }
        else 
        {
            Debug.Log("신규 유저");
            //신규유저
            //StageInfo객체 만들어서 리스트에 넣고 JSON으로 저장 
            int i = 0; 
            foreach (var data in DataManager.instance.GetStageDatas())
            {
                StageInfo info = null;

                if (i == 0)
                    info = new StageInfo(data.id, 0); //open 
                else
                    info = new StageInfo(data.id);  //lock 

                InfoManager.instance.StageInfos.Add(info);
                i++;
            }
            //저장 
            InfoManager.instance.SaveStageInfos();
        }

        var sum = InfoManager.instance.StageInfos.Sum(x => x.starsCount);
        Debug.LogFormat("sum: {0}", sum);

        this.director.Init();
    }
}
