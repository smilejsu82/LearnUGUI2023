using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public static readonly EventManager instance = new EventManager();

    public System.Action onBtnAdClick;
    public System.Action<int> onPurchaseChest;

    private EventManager()
    { 

    }


}
