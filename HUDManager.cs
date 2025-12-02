using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TMP_Text textLlenya;
    public TMP_Text textTemps;

    private float temps = 0f;

    void Update()
    {
        // Actualitzar llenya
        if (textLlenya != null)
        {
            textLlenya.text =
                "Llenya: " + GameManager.instance.llenyaRecollida +
                " / " + GameManager.instance.llenyaTotal;
        }

        // Actualitzar temps
        temps += Time.deltaTime;
        if (textTemps != null)
            textTemps.text = "Temps: " + Mathf.FloorToInt(temps) + "s";
    }

    public void ReiniciarTemps()
    {
        temps = 0f;
    }

    public float GetTempsActual()
    {
        return temps;
    }

    public void ActualitzarLlenya(int recollida, int total)
    {
        if (textLlenya != null)
            textLlenya.text = "Llenya: " + recollida + " / " + total;
    }
}
