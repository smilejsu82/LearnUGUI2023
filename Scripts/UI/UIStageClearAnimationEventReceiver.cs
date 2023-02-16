using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageClearAnimationEventReceiver : MonoBehaviour
{
    private int starCount = 3;
    public GameObject[] arrStars;
    public ParticleSystem burstStars;   //3개 받았을 경우 

    public void AppearStars()
    {
        Debug.Log("<color=yellow>AppearStars</color>");
        for (int i = 0; i < this.arrStars.Length; i++)
            this.arrStars[i].SetActive(false);

        for (int i = 0; i < this.starCount; i++) {
            var delay = i * 0.1f;
            this.StartCoroutine(this.WaitForAppearStars(i, delay));
        }
    }

    private IEnumerator WaitForAppearStars(int idx, float delay) {
        yield return new WaitForSeconds(delay); //0, 0.1, 0.2 
        this.arrStars[idx].SetActive(true);
        if (idx == 2) {
            yield return new WaitForSeconds(0.4f);  //0.6 - 0.2 

            Debug.Log("<color=cyan>Burst !!!!</color>");

            this.burstStars.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Debug.Log("disable");
        this.burstStars.gameObject.SetActive(false);
    }

}
