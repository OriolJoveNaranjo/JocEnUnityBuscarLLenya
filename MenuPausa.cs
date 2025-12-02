using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MenuPausa : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject hudCanvas;

    [SerializeField] private TMP_Text textLlenyaPausa;
    [SerializeField] private TMP_Text textTempsPausa;

    private HUDManager hudManager;
    private bool juegoPausado = false;

    private void Start()
    {
        if (menuPausa != null) 
            menuPausa.SetActive(false);

        // Busquem HUDManager automàticament
        hudManager = FindFirstObjectByType<HUDManager>();
        if (hudManager == null)
            Debug.LogWarning("No s'ha trobat HUDManager a l'escena");
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (juegoPausado) 
                Reanudar();
            else 
                Pausar();
        }
    }

    public void Pausar()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (hudCanvas != null) 
            hudCanvas.SetActive(false);

        // Actualitzar dades del HUD al menú pausa
        ActualitzarDadesPausa();

        if (menuPausa != null) 
            menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (hudCanvas != null) 
            hudCanvas.SetActive(true);

        if (menuPausa != null) 
            menuPausa.SetActive(false);
    }

    private void ActualitzarDadesPausa()
    {
        if (hudManager == null)
        
           hudManager = FindObjectOfType<HUDManager>(true); // busca inclús inactius           
        

        if (hudManager != null)
        {
            // Llenya
            if (textLlenyaPausa != null)
            {
                textLlenyaPausa.text = 
                    "Llenya: " + 
                    GameManager.instance.llenyaRecollida + 
                    " / " + 
                    GameManager.instance.llenyaTotal;
            }

            // Temps
            if (textTempsPausa != null)
            {
                float temps = hudManager.GetTempsActual();
                textTempsPausa.text = "Temps: " + Mathf.FloorToInt(temps) + "s";
            }
        }
        else
        {
         Debug.LogWarning("HUDManager no trobat quan s'ha obert el menú pausa.");
        }
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
