using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager instance;
    [SerializeField]
    private Fade fade;
    [SerializeField,Header("フェードの時間")]
    private float fadeDuration = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 引数で指定したシーンへの遷移の準備
    /// </summary>
    /// <param name="nextSceneType"></param>
    public void PreparateNextScene(SceneType nextSceneType)
    {
        if (!fade)
        {
        StartCoroutine(LoadNextScene(nextSceneType));
        }
        else
        {
            fade.FadeIn(fadeDuration,() =>{ StartCoroutine(LoadNextScene(nextSceneType)); });
        }
    }

    /// <summary>
    /// 引数で指定したシーンへ遷移
    /// </summary>
    /// <param name="nextLoadSceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadNextScene(SceneType nextLoadSceneName)
    {
        SceneManager.LoadScene(nextLoadSceneName.ToString());
        yield return null;
    }

}
