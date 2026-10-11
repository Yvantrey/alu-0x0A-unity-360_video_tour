using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SwitchRooms : MonoBehaviour
{
    public GameObject livingRoomSphere;
    public GameObject cantinaSphere;
    public GameObject cubeSphere;
    public GameObject mezzanineSphere;

    public Button cantinaHotspot;
    public Button livingRoomHotspot;
    public Button cubeHotspotFromLiving;
    public Button cubeHotspotFromCantina;
    public Button cubeHotspotFromMezzanine;
    public Button mezzanineHotspot;

    public Animator fadeAnimator;

    private GameObject currentSphere;
    private GameObject[] allSpheres;

    void Start()
    {
        allSpheres = new GameObject[] { livingRoomSphere, cantinaSphere, cubeSphere, mezzanineSphere };

        if (livingRoomSphere == null) return;

        SetActiveSphere(livingRoomSphere);

        if (cantinaHotspot) cantinaHotspot.onClick.AddListener(() => StartSwitch(cantinaSphere));
        if (livingRoomHotspot) livingRoomHotspot.onClick.AddListener(() => StartSwitch(livingRoomSphere));
        if (cubeHotspotFromLiving) cubeHotspotFromLiving.onClick.AddListener(() => StartSwitch(cubeSphere));
        if (cubeHotspotFromCantina) cubeHotspotFromCantina.onClick.AddListener(() => StartSwitch(cubeSphere));
        if (cubeHotspotFromMezzanine) cubeHotspotFromMezzanine.onClick.AddListener(() => StartSwitch(cubeSphere));
        if (mezzanineHotspot) mezzanineHotspot.onClick.AddListener(() => StartSwitch(mezzanineSphere));
    }

    public void StartSwitch(GameObject targetSphere)
    {
        if (currentSphere == targetSphere) return;

        if (fadeAnimator != null)
            StartCoroutine(SwitchWithFade(targetSphere));
        else
            SwitchSphere(targetSphere);
    }

    public void SwitchToCantina() => StartSwitch(cantinaSphere);
    public void SwitchToLivingRoom() => StartSwitch(livingRoomSphere);
    public void SwitchToCube() => StartSwitch(cubeSphere);
    public void SwitchToMezzanine() => StartSwitch(mezzanineSphere);

    private IEnumerator SwitchWithFade(GameObject targetSphere)
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SwitchSphere(targetSphere);
        yield return new WaitForSeconds(0.5f);
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
    }

    public void SwitchSphere(GameObject targetSphere)
    {
        if (targetSphere == null) return;

        foreach (var sphere in allSpheres)
        {
            if (sphere != null)
                sphere.SetActive(false);
        }

        SetActiveSphere(targetSphere);
        Debug.Log(targetSphere.name + " sphere is now active");
    }

    void SetActiveSphere(GameObject sphere)
    {
        sphere.SetActive(true);
        currentSphere = sphere;

        // Restart the video so it plays from the beginning and avoids timestamp issues
        var vp = sphere.GetComponent<VideoPlayer>();
        if (vp != null)
        {
            vp.Stop();
            vp.Play();
        }
    }
}
