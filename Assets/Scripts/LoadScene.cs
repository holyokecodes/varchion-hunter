using System.Collections;
using System.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    
    public GameObject loadingScreen;
    public string sceneToLoad;
    public CanvasGroup canvasGroup;

    public string[] hints;
    public Text hintText;
    public bool giveHints = false;

    public Slider progressBar;
    public bool doProgressBar = false;
    public bool fadeVolume = false;

    public AudioSource music;

    public string destroyScene;
    public GameObject activateOnLoad;

    bool loadHasEnded = false;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == destroyScene && loadHasEnded)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(string scene)
    {
        if (scene != "") sceneToLoad = scene;
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 0.5f));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!operation.isDone)
        {
            if (doProgressBar) progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            if (giveHints)
            {
                hintText.text = hints[Random.Range(0, hints.Length)];
            }
            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 0.5f));
        loadingScreen.SetActive(false);
        if (activateOnLoad != null) activateOnLoad.SetActive(true);
        loadHasEnded = true;
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float startVolume = 0;
        float targetVolume;
        float time = 0;

        if (fadeVolume)
            startVolume = music.volume;

        if (targetValue == 0)
            targetVolume = 1;
        else
            targetVolume = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            if(fadeVolume)
                music.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
