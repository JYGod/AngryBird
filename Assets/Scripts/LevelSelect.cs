using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public Sprite levelBg;
    public List<GameObject> starList;

    private bool isSelect = false;
    private Image imageBg;

    private void Awake()
    {
        imageBg = GetComponent<Image>();
    }

    private void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {
            int numBefore = int.Parse(gameObject.name.Substring(5)) - 1; // 当前关卡的Level
            if (PlayerPrefs.GetInt("block" + numBefore) > 0) 
            {
                isSelect = true;
            }
        }
        if (isSelect)
        {
            imageBg.overrideSprite = levelBg;
            transform.Find("num").gameObject.SetActive(true);
            ShowStar();
        }
    }

    private void ShowStar()
    {
        int count = PlayerPrefs.GetInt(gameObject.name);
        //Debug.Log(gameObject.name + ": " + count);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                starList[i].SetActive(true);
            }
        }
    }

    public void OnBlockClick()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("level", gameObject.name);
            SceneManager.LoadScene(1); // 跳转到游戏场景 
        }
    }

}
