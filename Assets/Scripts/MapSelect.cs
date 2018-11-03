using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour {

    public GameObject lockGo;
    public GameObject starGo;
    public GameObject panel;
    public GameObject map;

    public int starNumber = 0;

    private bool isSelect = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("totalStar", 0) >= starNumber)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            lockGo.SetActive(false);
            starGo.SetActive(true);
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
}
