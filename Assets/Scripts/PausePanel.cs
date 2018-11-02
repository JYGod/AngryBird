using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    public GameObject btnPause;

    private Animator anim;
    private static int SCENE_LOADING = 0;
    private static int SCENE_GAME = 1;
    private static int SCENE_LEVEL = 2;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SCENE_GAME);
    }

    /// <summary>
    /// 暂停播放动画
    /// </summary>
    public void Pause()
    {
        //Debug.Log("click pause");
        btnPause.SetActive(false);
        anim.SetBool("isPause", true);
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SCENE_LEVEL);
    }

    public void PauseAnim()
    {
        Time.timeScale = 0; 
    }

    public void ResumeAnim()
    {
        btnPause.SetActive(true);
    }
    

}
