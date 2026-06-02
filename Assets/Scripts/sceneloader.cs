using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadIntranetTour()
    {
        LoadSceneWithCheck("IntranetTourScene");
    }

    public void LoadCustomTour()
    {
        LoadSceneWithCheck("CustomCampusTourScene");
    }

    public void LoadMainMenu()
    {
        LoadSceneWithCheck("MainMenuScene");
    }

    private void LoadSceneWithCheck(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings. Add it via File > Build Profiles.");
        }
    }
}