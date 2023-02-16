using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameScene�ȿ� �ִ� ��� ������Ʈ�� ���� 
public class GameMain : MonoBehaviour
{
    public UIGame uiGame;   //GameScene�� �ִ� ��� UI�� ����
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

        //Canvas�� Render Mode�� Screen Space - Camera �ϰ�� 
        //Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(this.uiCam, this.enemy.hpPoint.position);
        //RectTransform canvasRectTransform = this.canvas.GetComponent<RectTransform>();
        //Vector2 localPoint;
        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, this.uiCam, out localPoint))
        //{
        //    //healthBar�� local��ġ�� localPoint �̵� 
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
        var go = Instantiate(this.enemyPrefab); //������ �ν��Ͻ� ���� 
        go.transform.position = new Vector3(5, 0.2f, 0);    //�ʱ� ��ġ 
        return go.GetComponent<Enemy>();
    }

    private UIHealthBar CreateUIHealthBar()
    {
        var go = Instantiate(this.uiHealthBarPrefab, this.canvas.transform);

        Debug.LogFormat("CreateUIHealthBar: {0}", go);

        return go.GetComponent<UIHealthBar>();
    }
}
