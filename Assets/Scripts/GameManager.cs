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
        Init();
    }

    public void ShowStar()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        for (int i = 0; i < birdList.Count + 1; i++)
        {
            yield return new WaitForSeconds(0.5f);
            starList[i].SetActive(true);
        }
    }

    public void Replay()
    {
        Debug.Log("replay");
        SceneManager.LoadScene(SCENE_GAME);
    }

    public void Home()
    {
        SceneManager.LoadScene(SCENE_LEVEL);
    }

}
