using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSync : MonoBehaviour {

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
        Invoke("Load", 2f);
    }

    private void Load()
    {
        SceneManager.LoadScene(2);
    }
}
 