using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo 
{
    public int id;
    public int state;
    public int starsCount;

    public StageInfo(int id, int state = 2, int starsCount = 0)     //2 : lock 
    {
        this.id = id;
        this.state = state;
        this.starsCount = starsCount;
    }
}
