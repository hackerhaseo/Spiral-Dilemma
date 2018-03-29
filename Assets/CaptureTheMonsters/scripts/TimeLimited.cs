using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimited : MonoBehaviour {

    public float maxTime;
    public float timeReward;

    [Space(10)]
    public Text showTime;
    public Text rewardText;

    float time;
    GameManager gameM;

    public static bool addTime;

    private void Awake()
    {
        time = maxTime;

        gameM = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        Timer();
    }

    void Timer()
    {
        if (time >= 0) time -= Time.deltaTime;

        if (addTime)
        {
            time += timeReward;
            addTime = false;
            StartCoroutine(ShowReward());
        }

        if (time <= 0) gameM.GameOver("The time is over!");

        if (time < 10) showTime.color = Color.red;
        else showTime.color = Color.white;

        showTime.text = time.ToString("F0");
    }

    IEnumerator ShowReward()
    {
        rewardText.gameObject.SetActive(true);
        rewardText.text = " +" + timeReward;
        rewardText.color = Color.yellow;

        yield return new WaitForSeconds(2);

        rewardText.gameObject.SetActive(false);
    }
}
