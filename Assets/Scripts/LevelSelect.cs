using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public Sprite levelBg;

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
        if (isSelect)
        {
            imageBg.overrideSprite = levelBg;
            transform.Find("num").gameObject.SetActive(true);
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
