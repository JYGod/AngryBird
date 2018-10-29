using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<Bird> birdList;
    public List<Pig> pigList;

    public static GameManager _instance;

    private Vector3 originPosition;

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
            // win
            return;
        }
        if (birdList.Count == 0)
        {
            // lose
            return;
        }
        Init();
    }

}
