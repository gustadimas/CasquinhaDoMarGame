using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int diasCompletos;
    public static int proximaEtapa;

    private void Awake()
    {
#if !UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneId)
    {
        if (sceneId >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("ID da cena fora do index. Verifique a configuração de cenas.");
            return;
        }
        SceneManager.LoadScene(sceneId);
    }

    public int CheckSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}