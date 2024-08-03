using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int diasCompletos = 0;

    private void Awake()
    {
#if !UNITY_EDITOR
		Application.targetFrameRate = 31;
#endif
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public int CheckSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
