using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Variables de partida")]
    public int llenyaRecollida = 0;
    public int llenyaTotal = 10;   // VALOR FIX DEFINITIU
    public bool gameOver = false;
    public bool gameWon = false;

    [Header("Referències UI")]
    public GameObject menuFinalPartida;
    private GameObject hudCanvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ReasignarReferencias());
    }

    IEnumerator ReasignarReferencias()
    {
        yield return null;

        // Buscar HUDCanvas a qualsevol escena
        if (hudCanvas == null)
            hudCanvas = GameObject.Find("HUDCanvas") ??
                        FindAnyObjectByType<HUDManager>(FindObjectsInactive.Include)?.gameObject;

        if (hudCanvas != null)
            hudCanvas.SetActive(true);
        else
            Debug.LogWarning("No s'ha trobat el HUDCanvas a l'escena.");

        // Buscar menú final
        if (menuFinalPartida == null)
        {
            var finalManager = FindAnyObjectByType<FinalPartidaManager>(FindObjectsInactive.Include);
            if (finalManager != null)
            {
                menuFinalPartida = finalManager.gameObject;
                menuFinalPartida.SetActive(false);
            }
        }

        // Amagar menú pausa si existeix a l'escena
        GameObject menuPausaObj = GameObject.Find("MenuPausa");
        if (menuPausaObj != null)
            menuPausaObj.SetActive(false);
    }

    // Afegir llenya
    public void AfegirLlenya(int quantitat)
    {
        llenyaRecollida += quantitat;

        var hud = FindFirstObjectByType<HUDManager>();
        if (hud != null)
            hud.ActualitzarLlenya(llenyaRecollida, llenyaTotal);
    }

    // PERDRE PARTIDA
    public void PerdrePartida()
    {
        gameOver = true;
        gameWon = false;

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (hudCanvas != null)
            hudCanvas.SetActive(false);

        if (menuFinalPartida != null)
            menuFinalPartida.SetActive(true);
    }

    // GUANYAR PARTIDA
    public void GuanyarPartida()
    {
        gameWon = true;
        gameOver = false;

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (hudCanvas != null)
            hudCanvas.SetActive(false);

        if (menuFinalPartida != null)
            menuFinalPartida.SetActive(true);
    }

    // Reiniciar dades
    public void ReiniciarDades()
    {
        llenyaRecollida = 0;
        gameOver = false;
        gameWon = false;

        Time.timeScale = 1f;

        if (hudCanvas != null)
        {
            var hud = hudCanvas.GetComponent<HUDManager>();
            if (hud != null)
            {
                hud.ReiniciarTemps();
                hudCanvas.SetActive(true);
            }
        }
    }

    public float GetTempsJoc()
    {
        HUDManager hud = FindAnyObjectByType<HUDManager>();
        if (hud != null)
            return hud.GetTempsActual();

        return 0f;
    }
}
