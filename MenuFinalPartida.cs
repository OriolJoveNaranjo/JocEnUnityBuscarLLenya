using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinalPartida : MonoBehaviour
{
    public void VolverAJugar()
    {
        // Reanudar el tiempo por si estaba pausado
        Time.timeScale = 1f;

        // Reiniciar las variables del GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.ReiniciarDades();

            // Seguridad adicional: eliminar posibles duplicados del GameManager
            foreach (GameManager gm in Object.FindObjectsByType<GameManager>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (gm != GameManager.instance)
                    Destroy(gm.gameObject);
            }
        }

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
        Debug.Log("Saliendo del juego...");

    #if UNITY_EDITOR
        // Si estás probando en el editor, detiene el modo Play
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    // Si estás en una build, cierra la aplicación
        Application.Quit();
    #endif
    }
    }
