using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitchButton : MonoBehaviour
{
    private bool state; //false , true 
    private Button btn;
    public GameObject[] offOn;  //0 : off, 1 : on 

    void Start()
    {
        this.btn = this.GetComponent<Button>();
        this.btn.onClick.AddListener(() => {
            Debug.Log("click!");
            this.SwitchState();
        });

        this.Init();
    }

    private void Init()
    {
        this.state = false;
        this.Off();
    }

    private void On()
    {
        offOn[0].SetActive(false);
        offOn[1].SetActive(true);
    }

    private void Off()
    {
        offOn[0].SetActive(true);
        offOn[1].SetActive(false);
    }

    public void SwitchState()
    {
        var current = !this.state;
        Debug.LogFormat("prev: {0}, current: {1}", this.state, current);
        if (current) this.On();
        else this.Off();
        this.state = current;   //변경된 상태로 갱신 
    }
}
