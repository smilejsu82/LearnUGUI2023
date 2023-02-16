using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameScene안에 있는 모든 오브젝트를 관리 
public class GameMain : MonoBehaviour
{
    public UIGame uiGame;   //GameScene에 있는 모든 UI를 관리
    public GameObject enemyPrefab;
    public GameObject uiHealthBarPrefab;

    private Enemy enemy;
    private UIHealthBar uiHealthBar;

    private Canvas canvas;
    //private Camera uiCam;

    void Start()
    {
        //this.uiCam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        //Debug.LogFormat("<color=yellow>uiCam: {0}</color>", this.uiCam);

        this.canvas = this.uiGame.GetComponent<Canvas>();

        this.CreateEnemyWithHealthBar();

        //Canvas의 Render Mode가 Screen Space - Camera 일경우 
        //Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(this.uiCam, this.enemy.hpPoint.position);
        //RectTransform canvasRectTransform = this.canvas.GetComponent<RectTransform>();
        //Vector2 localPoint;
        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, this.uiCam, out localPoint))
        //{
        //    //healthBar의 local위치를 localPoint 이동 
        //    this.uiHealthBar.GetComponent<RectTransform>().localPosition = localPoint;
        //}

    }

    private void CreateEnemyWithHealthBar()
    {
        var enemy = this.CreateEnemy();
        var healthbar = this.CreateUIHealthBar();

        Debug.LogFormat("CreateEnemyWithHealthBar: {0}", healthbar);

        enemy.AddHealthBar(healthbar);
    }

    private Enemy CreateEnemy()
    {
        var go = Instantiate(this.enemyPrefab); //프리팹 인스턴스 생성 
        go.transform.position = new Vector3(5, 0.2f, 0);    //초기 위치 
        return go.GetComponent<Enemy>();
    }

    private UIHealthBar CreateUIHealthBar()
    {
        var go = Instantiate(this.uiHealthBarPrefab, this.canvas.transform);

        Debug.LogFormat("CreateUIHealthBar: {0}", go);

        return go.GetComponent<UIHealthBar>();
    }
}
