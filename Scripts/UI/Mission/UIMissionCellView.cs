using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMissionCellView : MonoBehaviour
{
    public Image imgIcon;
    public TMP_Text txtName;
    public TMP_Text txtGoalDesc;
    public TMP_Text txtProgress;
    public Image imgRewardItemIcon;
    public TMP_Text txtRewardItemAmount;
    public TMP_Text txtRewardItemAmountDone;

    public GameObject[] arrStateGo;

    public Slider slider;
    public GameObject rewardItemGo;

    private MissionInfo info;
    private MissionData data;

    public Button btnGetReward;
    public Button btn; //test

    private void Awake()
    {
        this.btn = this.GetComponent<Button>();
        this.btnGetReward.onClick.AddListener(() => {

            //change state
            this.info.state = (int)UIEnums.eMissionState.Complete;

            this.SetState(UIEnums.eMissionState.Complete);

            Debug.LogFormat("id: {0}, state: {1}", this.info.id, this.info.state);

            //save
            InfoManager.instance.SaveMissionInfos();

        });
    }

    public void Init(MissionInfo info)
    {
        this.info = info;
        this.data = DataManager.instance.GetDataDic<MissionData>()[this.info.id];
        var atlas = AtlasManager.instance.GetAtlasByName("UIMission");
        var sprite = atlas.GetSprite(this.data.sprite_name);
        this.imgIcon.sprite = sprite;
        this.imgIcon.SetNativeSize();
        this.txtName.text = this.data.name;
        this.txtGoalDesc.text = string.Format(this.data.goal_desc, this.data.goal_val);
        this.txtProgress.text = string.Format("{0} / {1}", this.info.progress, this.data.goal_val);

        //보상 아이템 
        var rewardItemData = DataManager.instance.GetDataDic<RewardItemData>()[this.data.reward_item_id];
        this.imgRewardItemIcon.sprite = atlas.GetSprite(rewardItemData.sprite_name);
        this.imgRewardItemIcon.SetNativeSize();

        this.txtRewardItemAmountDone.text = this.txtRewardItemAmount.text = this.data.reward_item_amount.ToString();

        this.SetState((UIEnums.eMissionState)this.info.state);
    }

    public void SetState(UIEnums.eMissionState state)
    {

        foreach (var go in this.arrStateGo) go.SetActive(false);

        this.arrStateGo[(int)state].SetActive(true);

        switch (state) {
            case UIEnums.eMissionState.Doing:
                var per = (float)this.info.progress / this.data.goal_val;
                Debug.LogFormat("per: {0}", per);
                this.slider.value = per;

                this.slider.gameObject.SetActive(true);
                this.rewardItemGo.SetActive(false);
                break;

            case UIEnums.eMissionState.Done:
                this.slider.gameObject.SetActive(false);
                this.rewardItemGo.SetActive(true);
                break;

            case UIEnums.eMissionState.Complete:
                this.slider.gameObject.SetActive(false);
                this.rewardItemGo.SetActive(false);
                break;
        }

    }
}
