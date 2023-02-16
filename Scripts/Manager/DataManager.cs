using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    //컬렉션 초기화 (링큐 사용 안할때)
    private Dictionary<int, ShopData> dicShopData;// = new Dictionary<int, ShopData>();
    private Dictionary<int, ItemData> dicItemData;
    private Dictionary<int, MissionData> dicMissionData;
    private Dictionary<int, RewardItemData> dicRewardItemData;
    private Dictionary<int, CurrencyData> dicCurrencyData;
    private Dictionary<int, DailyRewardData> dicDailyRewardData;


    //test
    private Dictionary<string, Dictionary<int, RawData>> dic = new Dictionary<string, Dictionary<int, RawData>>();

    private Dictionary<string, string> pathDic = new Dictionary<string, string>();

    //생성자 
    private DataManager() {
        pathDic.Add(typeof(MissionData).ToString(), "mission_data");
        pathDic.Add(typeof(RewardItemData).ToString(), "reward_item_data");
    }

    public ShopData GetShopData(int id)
    {
        return this.dicShopData[id];
    }

    public void LoadShopData() {
        //TextAsset을 로드 
        TextAsset asset = Resources.Load<TextAsset>("Data/shop_data");  //경로를 포함한 확장자를 뺀 이름 
        //asset의 문자열 
        var json = asset.text;
        Debug.Log(json);

        //역직렬화 
        ShopData[] arrShopDatas = JsonConvert.DeserializeObject<ShopData[]>(json);

        this.dicShopData = arrShopDatas.ToDictionary(x => x.id);    //새로운 사전 객체가 생성 반환 

        //foreach (var data in arrShopDatas) {
        //    this.dicShopData.Add(data.id, data);
        //}

        Debug.LogFormat("shop data loaded : {0}", this.dicShopData.Count);  //5
    }

    public List<ShopData> GetShopDatas()
    {
        return this.dicShopData.Values.ToList();
    }

    public void LoadItemData()
    {
        //Resources
        //Data/item_data
        TextAsset asset = Resources.Load<TextAsset>("Data/item_data");
        string json = asset.text;
        Debug.Log(json);
        //역직렬화 
        ItemData[] arr = JsonConvert.DeserializeObject<ItemData[]>(json);
        //foreach, for 돌리면서 dic에 추가 (dic 인스턴스화 필요)

        //Linq 사용 (dic 인스턴화 필요 x)
        this.dicItemData = arr.ToDictionary(x => x.id);    //id를 키로 
        Debug.Log("item data loaded.");
        Debug.LogFormat("item data count: <color=yellow>{0}</color>", this.dicItemData.Count);

    }

    public ItemData GetItemData(int id)
    {
        if (this.dicItemData.ContainsKey(id)) {
            return this.dicItemData[id];
        }

        Debug.LogFormat("key ({0}) not found.", id);
        return null;
    }

    public ItemData GetRandomItemData()
    {
        //랜덤 아이템 획득 
        //0 ~ (count -1)  + 100 
        var randId = Random.Range(0, this.dicItemData.Count) + 100;   //0 ~ 22 
        return this.GetItemData(randId);
    }

    public void LoadMissionData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/mission_data");
        string json = asset.text;
        MissionData[] arr = JsonConvert.DeserializeObject<MissionData[]>(json);
        this.dicMissionData = arr.ToDictionary(x => x.id);    
        Debug.LogFormat("mission data loaded : <color=yellow>{0}</color>", this.dicMissionData.Count);
    }

    public MissionData GetMissionData(int id)
    {
        return this.dicMissionData[id];
    }

    public void LoadRewardItemData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/reward_item_data");
        string json = asset.text;
        RewardItemData[] arr = JsonConvert.DeserializeObject<RewardItemData[]>(json);
        this.dicRewardItemData = arr.ToDictionary(x => x.id);
        Debug.LogFormat("reward item data loaded : <color=yellow>{0}</color>", this.dicRewardItemData.Count);
    }

    public RewardItemData GetRewardItemData(int id)
    {
        return this.dicRewardItemData[id];
    }

    public void LoadRewardDailyData() {

        var asset = Resources.Load<TextAsset>("Data/daily_reward_data");
        var json = asset.text;
        //역직렬화 
        var arr = JsonConvert.DeserializeObject<DailyRewardData[]>(json);
        this.dicDailyRewardData = arr.ToDictionary(x => x.id);

        Debug.LogFormat("daily reward data loaded : <color=yellow>{0}</color>", this.dicDailyRewardData.Count);
    }

    public DailyRewardData GetDailyRewardData(int id)
    {
        return this.dicDailyRewardData[id];
    }

    //dicDailyRewardData
    public int GetDailyRewardCount()
    {
        return this.dicDailyRewardData.Count;
    }

    public void LoadCurrencyData()
    {
        var asset = Resources.Load<TextAsset>("Data/currency_data");
        var json = asset.text;
        //역직렬화 
        var arr = JsonConvert.DeserializeObject<CurrencyData[]>(json);
        this.dicCurrencyData = arr.ToDictionary(x => x.id);

        Debug.LogFormat("currency data loaded : <color=yellow>{0}</color>", this.dicCurrencyData.Count);
    }

    public CurrencyData GetCurrencyData( int id)
    {
        return this.dicCurrencyData[id];
    }



    public void LoadData<T>() where T : RawData
    {
        Debug.LogFormat("LoadData: {0}", typeof(T).ToString());
        var key = typeof(T).ToString();

        var path = this.pathDic[key];

        TextAsset asset = Resources.Load<TextAsset>(string.Format("Data/{0}", path));

        string json = asset.text;
        T[] arr = JsonConvert.DeserializeObject<T[]>(json);

        var a = arr.ToDictionary(x => x.id, x=>(RawData)x);

        if (!dic.ContainsKey(key))
        {
            this.dic.Add(key, a);
        }

        Debug.LogFormat("key: {0}", key);
        Debug.LogFormat("{0} loaded : <color=yellow>{1}</color>", path, this.dic[key].Count);

    }

    public Dictionary<int, T> GetDataDic<T>() where T : RawData
    {
        var key = typeof(T).ToString();

        var a = this.dic[key];

        return a.ToDictionary(x => x.Key, x => (T)x.Value);
    }

    public T GetData<T>(int id) where T : RawData
    {
        var key = typeof(T).ToString();

        var a = this.dic[key];

        return a.ToDictionary(x => x.Key, x => (T)x.Value)[id];
    }

    public IEnumerable<DailyRewardData> GetDailyRewardDatas()
    {
        return dicDailyRewardData.Values;
    }

}
