using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalPartidaManager : MonoBehaviour
{
    public TMP_Text resultatText;
    public TMP_Text tempsText;
    public TMP_Text llenyaText;

    private void OnEnable()
    {
        if (GameManager.instance == null)
            return;

        // Resultat
        if (GameManager.instance.gameWon)
            resultatText.text = "HAS GUANYAT!";
        else
            resultatText.text = "HAS PERDUT";

        // Temps
        float temps = GameManager.instance.GetTempsJoc();
        tempsText.text = "Temps: " + Mathf.FloorToInt(temps) + "s";

        // Llenya recollida i total
        llenyaText.text = "Llenya: " +
            GameManager.instance.llenyaRecollida + " / " + GameManager.instance.llenyaTotal;
    }

    public void TornarAJugar()
    {
        Time.timeScale = 1f;
        GameManager.instance.ReiniciarDades();
        SceneManager.LoadScene("PrimeraPantallaJocUnity");
    }

    public void Sortir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
