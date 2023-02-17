using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

//싱글톤 클래스 
public class InfoManager 
{
    public class GameInfo
    {
        public string version;
    }

    public static readonly InfoManager instance = new InfoManager();

    public InventoryInfo InventoryInfo { get; set; }
    public List<MissionInfo> MissionInfos { get; set; }
    public List<RewardDailyInfo> RewardDailyInfos { get; set; }
    public List<StageInfo> StageInfos { get; set; }

    private GameInfo gameInfo;

    private const string MISSION_INFOS_PATH = "mission_infos";
    private const string STAGE_INFOS_PATH = "stage_infos";


    private InfoManager() { }

    public void Init() {
        //this.MissionInfos = new List<MissionInfo>();
        //foreach (var data in DataManager.instance.GetDataDic<MissionData>().Values) {
        //    this.MissionInfos.Add(new MissionInfo(data.id));
        //}
        //this.SaveMissionInfos();

        this.StageInfos = new List<StageInfo>();
        //this.RewardDailyInfos = new List<RewardDailyInfo>();

    }

    public StageInfo GetStageInfo(int id)
    {
        return this.StageInfos.Find(x => x.id == id);
    }

    public void LoadStageInfos()
    {
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, STAGE_INFOS_PATH);
        var json = File.ReadAllText(path);
        //역직렬화 
        this.StageInfos = JsonConvert.DeserializeObject<StageInfo[]>(json).ToList();
        Debug.LogFormat("<color=yellow>[load success] {0}.json</color>", STAGE_INFOS_PATH);
    }


    public void SaveStageInfos()
    {
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, STAGE_INFOS_PATH);
        //직렬화 
        var json = JsonConvert.SerializeObject(this.StageInfos);
        Debug.Log(json);
        //파일로 저장 
        File.WriteAllText(path, json);
        Debug.LogFormat("<color=yellow>[save success] {0}.json</color>", STAGE_INFOS_PATH);
    }

    public void SaveMissionInfos()
    {
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, MISSION_INFOS_PATH);
        //직렬화 
        var json = JsonConvert.SerializeObject(this.MissionInfos);
        Debug.Log(json);
        //파일로 저장 
        File.WriteAllText(path, json);
        Debug.LogFormat("<color=yellow>[save success] {0}.json</color>", MISSION_INFOS_PATH);
    }

    public void LoadMissionInfos()
    {
        var path = string.Format("{0}/{1}.json", Application.persistentDataPath, MISSION_INFOS_PATH);
        var json = File.ReadAllText(path);
        //역직렬화 
        this.MissionInfos = JsonConvert.DeserializeObject<MissionInfo[]>(json).ToList();
        Debug.LogFormat("<color=yellow>[load success] {0}.json</color>", MISSION_INFOS_PATH);
    }

    public MissionInfo GetMissionInfo(int id) {
        return this.MissionInfos.Find(x => x.id == id);
    }


    public void SaveInventoryInfo()
    {
        var path = string.Format("{0}/inventory_info.json", Application.persistentDataPath);
        //직렬화 
        var json = JsonConvert.SerializeObject(this.InventoryInfo);

        Debug.Log("---------------------");
        Debug.Log(json);
        Debug.Log("---------------------");

        //파일로 저장 
        File.WriteAllText(path, json);
        Debug.Log("<color=yellow>[save success] inventory_info.json</color>");
    }

    public void LoadInventoryInfo()
    {
        var path = string.Format("{0}/inventory_info.json", Application.persistentDataPath);
        var json = File.ReadAllText(path);
        //역직렬화 
        this.InventoryInfo = JsonConvert.DeserializeObject<InventoryInfo>(json);
        Debug.Log("<color=yellow>[load success] inventory_info.json</color>");
    }

    public bool IsNewbie()
    {
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);

        Debug.LogFormat("<color=cyan>{0}</color>", path);
        Debug.LogFormat("<b><color=yellow>IsNewbie: {0}</color></b>", File.Exists(path));

        if (File.Exists(path))
        {    
            var json = File.ReadAllText(path);
            this.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);

            return false;
        }
        else 
        {
            this.gameInfo = new GameInfo();
            this.gameInfo.version = Application.version;
            var json = JsonConvert.SerializeObject(this.gameInfo);
            File.WriteAllText(path, json);

            return true;
        }
    }

    public void SaveRewardDailyInfos()
    {
        var path = string.Format("{0}/daily_reward_info.json", Application.persistentDataPath);
        //직렬화 
        var json = JsonConvert.SerializeObject(this.RewardDailyInfos);

        Debug.Log(json);

        //파일로 저장 
        File.WriteAllText(path, json);
        Debug.Log("<color=yellow>[save success] daily_reward_data.json</color>");
    }

    public void LoadRewardDailyInfos()
    {
        var path = string.Format("{0}/daily_reward_info.json", Application.persistentDataPath);
        var json = File.ReadAllText(path);
        //역직렬화 
        var arr = JsonConvert.DeserializeObject<RewardDailyInfo[]>(json);
        this.RewardDailyInfos = arr.ToList();

        Debug.Log("<color=yellow>[load success] daily_reward_info.json</color>");
    }

    public RewardDailyInfo GetRewardDailyInfo(int id)
    {
        return this.RewardDailyInfos.Find(x => x.id == id);
    }


}
