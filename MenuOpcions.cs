using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOpcions : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Toggle toggleMusica;
    [SerializeField] private TMP_Dropdown selectorNivell;
    [SerializeField] private GameObject menuPrincipal;

    private AudioSource musicaFons;

    private void Start()
    {
        // Buscar la música amb el tag "Musica"
        GameObject objMusica = GameObject.FindGameObjectWithTag("Musica");
        if (objMusica != null)
        {
            musicaFons = objMusica.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("No s'ha trobat cap objecte amb el tag 'Musica'!");
        }

        // Carregar estat guardat
        bool musicaActivada = PlayerPrefs.GetInt("MusicaActivada", 1) == 1;

        // Aplicar estat a la música
        if (musicaFons != null)
        {
            if (musicaActivada) musicaFons.Play();
            else musicaFons.Pause();
        }

        // Configurar el toggle
        if (toggleMusica != null)
        {
            toggleMusica.isOn = musicaActivada;
            toggleMusica.onValueChanged.AddListener(CanviarMusica);
        }

        // Configurar el selector de nivell
        if (selectorNivell != null)
        {
            string nivellGuardat = PlayerPrefs.GetString("NivellSeleccionat", selectorNivell.options[0].text);
            int index = selectorNivell.options.FindIndex(o => o.text == nivellGuardat);

            if (index >= 0)
                selectorNivell.value = index;

            selectorNivell.onValueChanged.AddListener(CanviarNivell);

            // Si no existeix encara, guardar la selecció inicial
            if (!PlayerPrefs.HasKey("NivellSeleccionat"))
            {
                string nivellInicial = selectorNivell.options[selectorNivell.value].text;
                PlayerPrefs.SetString("NivellSeleccionat", nivellInicial);
            }
        }
    }

    // Activar/desactivar música
    private void CanviarMusica(bool activat)
    {
        if (musicaFons == null) return;

        if (activat) musicaFons.Play();
        else musicaFons.Pause();

        PlayerPrefs.SetInt("MusicaActivada", activat ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Guardar nivell seleccionat
    private void CanviarNivell(int index)
    {
        string nomNivell = selectorNivell.options[index].text;
        PlayerPrefs.SetString("NivellSeleccionat", nomNivell);
        PlayerPrefs.Save();
        Debug.Log("Nivell seleccionat: " + nomNivell);
    }

    // Tornar al menú principal (si el menú principal és una escena)
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    // Tornar al menú principal (si és un canvas)
    public void Tornar()
    {
        gameObject.SetActive(false);
        if (menuPrincipal != null)
            menuPrincipal.SetActive(true);
    }
}
