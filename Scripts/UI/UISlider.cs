using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    public Slider slider;
    public System.Action<float> onSliderValueChanged;

    public void Awake()
    {
        Debug.Log("Awake");
    }

    public void Init(float val)
    {
        Debug.Log("Init");
        this.slider.value = val;
    }

    void Start()
    {
        Debug.Log("Start");
        this.slider.onValueChanged.AddListener((val) => {

            //some logic

            this.onSliderValueChanged(val);
        });
    }
}
