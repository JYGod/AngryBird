using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birdList;
    public List<Pig> pigList;
    public GameObject winGo;
    public GameObject loseGo;
    public GameObject[] starList;

    public static GameManager _instance;

    private Vector3 originPosition;

    private static int SCENE_LOADING = 0;
    private static int SCENE_GAME = 1;
    private static int SCENE_LEVEL = 2;
    private int numStar = 0;
    private int totalLevel = 10; // 总关卡数

    private void Awake()
    {
        _instance = this; // 单例
        if (birdList.Count > 0)
        {
            originPosition = birdList[0].transform.position;
        }
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        for (int i = 0; i < birdList.Count; i++)
        {
            if (i == 0)
            {
                birdList[i].transform.position = originPosition;
                birdList[i].enabled = true;
                birdList[i].springJoint2D.enabled = true;
                birdList[i].canMove = true;
            }
            else
            {
                birdList[i].enabled = false;
                birdList[i].springJoint2D.enabled = false;
            }
        }
    }

    public void NextBird()
    { 
        if (IsGameFinish())
        {
            ShowFinishPanel();
            return;
        }
        Init();
    }

    private void ShowFinishPanel()
    {
        if (pigList.Count <= 0) 
        {
            winGo.SetActive(true);
            return;
        }
        if (birdList.Count == 0)
        {
            loseGo.SetActive(true );
            return;
        }
    }

    private bool IsGameFinish()
    {
        return pigList.Count <= 0 || birdList.Count == 0;
    }

    public void ShowStar()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        for (numStar = 0; numStar < birdList.Count + 1; numStar++)
        {
            if (numStar >= starList.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.5f);
            starList[numStar].SetActive(true);
        }
        Debug.Log("star: " + numStar);
    }

    public void Replay()
    {
        Debug.Log("replay");
        SaveData();
        SceneManager.LoadScene(SCENE_GAME);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(SCENE_LEVEL);
    }

    public void SaveData()
    {
        string level = PlayerPrefs.GetString("level");
        if (PlayerPrefs.GetInt(level, 0) < numStar)
        {
            PlayerPrefs.SetInt(level, numStar);
        }
        //Debug.Log("save: " + level + "\t " + numStar);
        // 存储所有星星个数
        int sum = 0;
        for (int i = 1; i <= totalLevel + 1; i++)
        {
            sum += PlayerPrefs.GetInt("block" + i, 0);
        }
        PlayerPrefs.SetInt("totalStar", sum);
        //Debug.Log("total: " + sum);
    }

}
