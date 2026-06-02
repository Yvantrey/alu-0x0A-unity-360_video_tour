using UnityEngine;
using System.Collections;

public class TourManager : MonoBehaviour
{
    public GameObject outsideView;
    public GameObject enterprise;
    public GameObject commonArea;
    public FadeManager fadeManager;

    public void ShowOutside()
    {
        if (fadeManager == null)
        {
            Debug.LogError("FadeManager not assigned to TourManager.");
            return;
        }
        fadeManager.FadeToScene(() => 
        {
            outsideView.SetActive(true);
            enterprise.SetActive(false);
            commonArea.SetActive(false);
        });
    }

    public void ShowEnterprise()
    {
        if (fadeManager == null)
        {
            Debug.LogError("FadeManager not assigned to TourManager.");
            return;
        }
        fadeManager.FadeToScene(() => 
        {
            outsideView.SetActive(false);
            enterprise.SetActive(true);
            commonArea.SetActive(false);
        });
    }

    public void ShowCommonArea()
    {
        if (fadeManager == null)
        {
            Debug.LogError("FadeManager not assigned to TourManager.");
            return;
        }
        fadeManager.FadeToScene(() => 
        {
            outsideView.SetActive(false);
            enterprise.SetActive(false);
            commonArea.SetActive(true);
        });
    }
}