using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private int SceneNumber;
    private string BMI = "BMI.exe";

    public void SceneChange()
    {
        switch (SceneNumber)
        {
            case 0:
                SceneManager.LoadScene("StartMenu");
                break;
            case 1:
                SceneManager.LoadScene("MatchingCard");
                break;
            case 2:
                SceneManager.LoadScene("Blink");
                break;
            case 3:
                SceneManager.LoadScene("Select");
                break;
            case 4:
                SceneManager.LoadScene("Kanahiroi");
                break;
            case 5:
                SceneManager.LoadScene("Block");
                break;
            case 6:
                SceneManager.LoadScene("ewomojini");
                break;
            case 7:
                Exit();
                break;
            case 8:
                SceneManager.LoadScene("PickupJapanese");
                break;
            case 9:
                SceneManager.LoadScene("WrongWord");
                break;
            case 10:
                System.Diagnostics.Process.Start(Application.dataPath + "/StreamingAssets/" + BMI);
                break;
        }
    }

    private void Exit()
    {
        #if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
                            UnityEngine.Application.Quit();
        #endif
    }
}
