using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInfo
{
    public int id;
    public int progress;    //count or amount 
    public int state;   //0 : doing, 1 : done, 2 : complete

    //»ý¼ºÀÚ 
    public MissionInfo(int id, int progress = 0, int state  = 0) {
        this.id = id;
        this.progress = progress;
        this.state = state;
    }
}
