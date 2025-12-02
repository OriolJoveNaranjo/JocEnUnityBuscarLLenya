using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Teletransportador : MonoBehaviour
{
    [Header("Configuració de teletransport")]
    [SerializeField] private string escenaDesti = "SegonaPantallaJocUnity";
    [SerializeField] private float retardCanvi = 1f;
    [SerializeField] private AudioClip soTeletransport;

    private AudioSource audioSource;
    private Image fadeImage;
    private bool enTransicio = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fadeImage = GameObject.Find("FadeCanvas")?.GetComponentInChildren<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enTransicio) return; // evitar doble detecció

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha entrat al trigger, canviant a " + escenaDesti);
            enTransicio = true;

            if (soTeletransport != null && audioSource != null)
                audioSource.PlayOneShot(soTeletransport);

            StartCoroutine(TransicioAmbFade());
        }
    }

    private IEnumerator TransicioAmbFade()
    {
        // Desactivar AudioListener abans del canvi d'escena
        var listener = Object.FindFirstObjectByType<AudioListener>();
        if (listener != null)
        {
            Debug.Log("Desactivant AudioListener antic per evitar duplicats...");
            listener.enabled = false;
        }

        // Efecte de fade
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                c.a = t;
                fadeImage.color = c;
                yield return null;
            }
        }

        // Esperar un moment abans de canviar
        yield return new WaitForSeconds(retardCanvi);

        // Carregar nova escena
        SceneManager.LoadScene(escenaDesti);
    }
}
