using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene los menús entre escenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Opcional: si quieres que el HUD se active solo en escenas jugables
        if (scene.name.Contains("Pantalla") || scene.name.Contains("Mundo"))
        {
            if (transform.Find("HUDCanvas") != null)
                transform.Find("HUDCanvas").gameObject.SetActive(true);
        }
        else
        {
            if (transform.Find("HUDCanvas") != null)
                transform.Find("HUDCanvas").gameObject.SetActive(false);
        }
    }
}
