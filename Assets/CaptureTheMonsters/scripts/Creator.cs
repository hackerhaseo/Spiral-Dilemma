using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Creator : MonoBehaviour {

    public GameObject linePrefab;

    public bool destroyImediately;
    public bool infiniteLine;

    [Header("Amount of line that can be draw")]
    public int maxPoints;
    [Header("Reward when you kill a enemy")]
    public int lineReward = 200;
    [Header("Max objects allowed on the same time")]
    public int maxObjects;

    [Header("UI")]
    public Text showPoints;
    public Text lineRewardText;

    [Header("time to destroy line after instantiate")]
    public float timeToDestroy;

    public static bool isAddingLine;

    Maker[] activeLine = new Maker[1000];
    GameObject[] lineGO = new GameObject[1000];

    GameManager manager;

    int count = -1;
    int objectCount = 0;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !manager.isGameOver) 
        {
            count++;
            if (maxPoints >= 0) lineGO[count] = Instantiate(linePrefab);
            activeLine[count] = lineGO[count].GetComponent<Maker>();
            objectCount++;
        }

        if (Input.GetMouseButtonUp(0) && !manager.isGameOver)
        {
            activeLine[count] = null;
            if (destroyImediately) Destroy(lineGO[count]);
        }

        if (count > -1 && activeLine[count] != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!infiniteLine && maxPoints > 0)
            {
                activeLine[count].UpdateLine(mousePos);
                maxPoints--;
            }
            else if (infiniteLine)
            {
                activeLine[count].UpdateLine(mousePos);
            }
            else return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(1);

        DestroyOverTime();

        ShowStatus();

        LineIsOver();

        if (isAddingLine)
        {
            maxPoints += lineReward;

            StartCoroutine(ShowLineReward());

            isAddingLine = false;
        }
    }

    IEnumerator ShowLineReward()
    {
        lineRewardText.gameObject.SetActive(true);
        lineRewardText.text = " +" + lineReward;
        lineRewardText.color = Color.yellow;

        yield return new WaitForSeconds(2);

        lineRewardText.gameObject.SetActive(false);

    }

    int objectIndex = -1;
    float time;

    void DestroyOverTime()
    {
        if (count > objectIndex)
        {
            time += Time.deltaTime;
            if (time > timeToDestroy || objectCount >= maxObjects)
            {
                objectIndex++;
                Destroy(lineGO[objectIndex]);
                time = 0;
                objectCount--;
            }
        }
    }

    public void DestroyAll()
    {
        if (count > 0)
        {
            for (int i = objectIndex - 1; i <= count; i++)
            {
                 Destroy(lineGO[i]);
            }
        }
    }

    void ShowStatus()
    {
        if (maxPoints < 10) showPoints.color = Color.red;

        if (!infiniteLine) showPoints.text = maxPoints.ToString();
        else showPoints.text = "infinite";
    }

    public void AddLine()
    {
        int max = maxPoints;
    }

    float timeGO;
    void LineIsOver()
    {
        if (maxPoints <= 0) manager.GameOver("You don't have more lines!");
    }
}
