using UnityEngine;
using System.Collections;

public class TourManager : MonoBehaviour
{
    public GameObject outsideView;
    public GameObject enterprise;
    public GameObject foodCourt;
    public FadeManager fadeManager;

    public void ShowOutside()
    {
        if (!ValidateSetup()) return;
        fadeManager.FadeToScene(() => SetActiveView(outsideView));
    }

    public void ShowEnterprise()
    {
        if (!ValidateSetup()) return;
        fadeManager.FadeToScene(() => SetActiveView(enterprise));
    }

    public void ShowFoodCourt()
    {
        if (!ValidateSetup()) return;
        fadeManager.FadeToScene(() => SetActiveView(foodCourt));
    }

    private void SetActiveView(GameObject target)
    {
        if (outsideView != null) outsideView.SetActive(outsideView == target);
        if (enterprise != null) enterprise.SetActive(enterprise == target);
        if (foodCourt != null) foodCourt.SetActive(foodCourt == target);
    }

    private bool ValidateSetup()
    {
        if (fadeManager == null)
        {
            Debug.LogError("FadeManager not assigned to TourManager.");
            return false;
        }
        if (outsideView == null) Debug.LogWarning("outsideView not assigned to TourManager.");
        if (enterprise == null) Debug.LogWarning("enterprise not assigned to TourManager.");
        if (foodCourt == null) Debug.LogWarning("foodCourt not assigned to TourManager.");
        return true;
    }
}
