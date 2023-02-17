using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnums
{
    public enum eItemType
    {
        Weapon,
        Shield,
        Armor,
        Accessory,
        Material,
        Quest,
        Potion
    }

    public enum eMissionState { 
        Doing,
        Done, 
        Complete
    }

    public enum eRewardItemType
    { 
        RewardItem = 100, 
        Currency = 300
    }

    public enum eStageCellViewState
    { 
        Open, Complete, Lock 
    }

}
