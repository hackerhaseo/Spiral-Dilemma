using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public int numberOfInitialEnemys;
    public float timeToInstantiateEachEnemy;

    [Header("Enemys prefabs")]
    public GameObject[] Prefabs;

    [Header("Drag UI stuffs here")]
    public Canvas gameplayCanvas;
    public Canvas gameOverCanvas;

    public GameObject panel;
    public Text gameOverText;

    private Creator creator;

    float time;

    [HideInInspector] public int enemyCount;

    [HideInInspector] public bool isGameOver;

    private void Awake()
    {
        creator = FindObjectOfType<Creator>();
    }

    private void Start()
    {
        for (int i = 0; i < numberOfInitialEnemys; i++)
        {
            int ran = Random.Range(0, 4);
            Instantiate(Prefabs[ran], new Vector3(0, 0, 0), Quaternion.identity);
        }

        gameplayCanvas.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);

        enemyCount = numberOfInitialEnemys;
    }

    private void FixedUpdate()
    {
        InstantiateOverTime();

        MaxEnemysAllow(5);
    }


    void InstantiateOverTime()
    {
        time += Time.deltaTime;

        if (time > timeToInstantiateEachEnemy)
        {
            float x = Random.Range(-2.5f, 2.5f);
            float y = Random.Range(4, -4);
            Vector3 ramdomPosition = new Vector3(x, y, 0);

            int ran = Random.Range(0, 4);
            Instantiate(Prefabs[ran], ramdomPosition, Quaternion.identity);

            time = 0;

            enemyCount++;
        }
    }


    int score;
    public Text showScore;
    public Text finalScore;
    public Text showHighscore;

    public void Score()
    {
        score++;
        showScore.text = score.ToString();
    }

    public void Highscore()
    {
        int highscore = PlayerPrefs.GetInt("highscore");
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
            finalScore.text = "Score: " + score;
            showHighscore.text = "Highscore: " + highscore;
        }
        else
        {
            finalScore.text = "Score: " + score;
            showHighscore.text = "Highscore: " + highscore;
            //PlayerPrefs.SetInt("highscore", 0);
        }
    }

    public void GameOver(string whyFail)
    {
        if (!isGameOver) creator.DestroyAll();

        isGameOver = true;

        gameplayCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);

        gameOverText.text = whyFail;

        StartCoroutine(ShowPanel());

        Highscore();
    }

    private  Vector3 targetPos;

    IEnumerator ShowPanel()
    {
        TargetPos();
        while (Vector3.Distance(panel.transform.position, targetPos) > 0.1f)
        {
            panel.transform.position = Vector3.Lerp(panel.transform.position, targetPos, Time.deltaTime * 0.03f);

            yield return null;
        }
    }

    void TargetPos()
    {
        GameObject image =  GameObject.Find("UICenter");
        targetPos = image.transform.position;
    }

    public void RestartGame()
    {
        isGameOver = false;

        SceneManager.LoadScene(1);
    }

    void MaxEnemysAllow(int max)
    {
        if (enemyCount > max)
        {
            this.GameOver("MaxEnemysAllowed!");
        }
    }

}
