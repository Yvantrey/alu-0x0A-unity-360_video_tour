using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade image not assigned to FadeManager.");
            return;
        }

        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
    }

    public void FadeToScene(System.Action onFadeComplete)
    {
        StartCoroutine(FadeInCoroutine(onFadeComplete));
    }

    private IEnumerator FadeInCoroutine(System.Action onFadeComplete)
    {
        yield return StartCoroutine(FadeIn());
        onFadeComplete?.Invoke();
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
