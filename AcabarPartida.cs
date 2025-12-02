using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AcabarPartida : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject menuFinal;
    public TMP_Text textoResultado;
    public TMP_Text textoTiempo;
    public Button botonSortir;
    public Button botonTornar;
    public HUDManager hudManager;

    private bool juegoTerminado = false;

    private void Start()
    {
        if (menuFinal != null)
            menuFinal.SetActive(false);

        if (botonSortir != null)
            botonSortir.onClick.AddListener(SortirDelJoc);

        if (botonTornar != null)
            botonTornar.onClick.AddListener(TornarAlMenu);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        if (other.CompareTag("Player"))
        {
            juegoTerminado = true;
            MostrarFinDePartida("HAS GUANYAT!");
        }
    }

    // Llamado también por GameManager en caso de perder o ganar
    public void MostrarFinDePartida(string missatge)
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float tempsFinal = 0f;

        if (hudManager != null)
            tempsFinal = hudManager.GetTempsActual();

        if (menuFinal != null)
        {
            menuFinal.SetActive(true);

            if (textoResultado != null)
                textoResultado.text = missatge;

            if (textoTiempo != null)
                textoTiempo.text = "Temps jugat: " + Mathf.FloorToInt(tempsFinal) + "s";
        }
    }

    public void TornarAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SortirDelJoc()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
