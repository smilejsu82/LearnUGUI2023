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

        //�ű����� , �������� �Ǻ� 
        //* ����Ʈ�� �ְ� : InfoManager.instance.stageInfos�� �߰� 
        if (System.IO.File.Exists(path))
        {
            Debug.Log("���� ����");
            //�������� 
            //JSON�ε� �ؼ� ������ȭ �ϰ� ����Ʈ�� �ְ� 
            InfoManager.instance.LoadStageInfos();

        }
        else 
        {
            Debug.Log("�ű� ����");
            //�ű�����
            //StageInfo��ü ���� ����Ʈ�� �ְ� JSON���� ���� 
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
            //���� 
            InfoManager.instance.SaveStageInfos();
        }

        var sum = InfoManager.instance.StageInfos.Sum(x => x.starsCount);
        Debug.LogFormat("sum: {0}", sum);

        this.director.Init();
    }
}
