using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButtons : MonoBehaviour {

    public void NextSceneButton()
    {
        SceneManager.LoadScene(++GameManager.level); // 다음 씬으로 이동
    }

    public void GameExitButton()
    {
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
