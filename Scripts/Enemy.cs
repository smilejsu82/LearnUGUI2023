using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform hpPoint;
    public UIHealthBar uiHealthBar;

    public void AddHealthBar(UIHealthBar uiHealthBar)
    {
        this.uiHealthBar = uiHealthBar;
        this.UpdateUIHealthBarPosition();
    }

    private void UpdateUIHealthBarPosition()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, this.hpPoint.position);
        this.uiHealthBar.transform.position = screenPoint;
    }

    private void Update()
    {
        this.transform.Translate(Vector3.left * 1.5f * Time.deltaTime);
        this.UpdateUIHealthBarPosition();
    }

}
