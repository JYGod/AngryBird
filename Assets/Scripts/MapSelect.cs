using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour {

    public GameObject lockGo;
    public GameObject starGo;
    public GameObject panel;
    public GameObject map;
    public int numStart = 1; // 开始关卡数
    public int numFinish = 12; // 结束关卡数
    public Text textStarCount;

    public int starNumber = 0;

    private bool isSelect = false; // 是否可选择

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //Debug.Log("all data has bean cleared");
    }

    private void Start()
    {
        //Debug.Log("totalStar: " + PlayerPrefs.GetInt("totalStar", 0));
        if (PlayerPrefs.GetInt("totalStar", 0) >= starNumber)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            lockGo.SetActive(false);
            starGo.SetActive(true);
            int count = 0;
            for (int i = numStart; i <= numFinish; i++)
            {
                count += PlayerPrefs.GetInt("block" + i, 0);
            }
            Debug.Log("count : " + count);
            textStarCount.text = count + "/" + (numFinish - numStart + 1) * 3;
        }
    }


    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public void OnMapClick()
    {
        if (isSelect)
        {
            map.SetActive(false);
            panel.SetActive(true);
        }
    }

    public void Home()
    {
        SceneManager.LoadScene(2);
    }
}
