using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestDateTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(DateTime.Now);
        string strNow = DateTime.Now.ToString("yyyy-MM-dd");
        //2023-02-16 
        Debug.Log(strNow);

        DateTime now = Convert.ToDateTime(strNow);
        Debug.Log(now);

        //비교 날짜 
        var christmas = new DateTime(2023, 12, 25);

        //날짜 비교 
        var result = DateTime.Compare(now, christmas);
        Debug.LogFormat("result: {0}", result);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
