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
            Debug.LogWarning("FadeManager: fadeImage not assigned. Transitions will happen instantly.");
            return;
        }

        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
    }

    public void FadeToScene(System.Action onFadeComplete)
    {
        // If no fade image, just execute the action immediately
        if (canvasGroup == null)
        {
            onFadeComplete?.Invoke();
            return;
        }
        StartCoroutine(FadeSequence(onFadeComplete));
    }

    private IEnumerator FadeSequence(System.Action onFadeComplete)
    {
        yield return StartCoroutine(Fade(0f, 1f));
        onFadeComplete?.Invoke();
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
