using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MenuPrincipal : MonoBehaviour
{
    [Header("Referencias de menús")]
    [SerializeField] private GameObject menuPrincipal;   // Canvas del menú principal
    [SerializeField] private GameObject menuOpciones;    // Canvas del menú d’opcions (desactivat per defecte)

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    private StarterAssets.StarterAssetsInputs _inputs;

    void Awake()
    {
        // Mostrar el cursor en el menú principal
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Buscar referències d’input
        _inputs = Object.FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
        _playerInput = Object.FindFirstObjectByType<PlayerInput>();
#endif

        // Evitar bloqueig del cursor mentre estem al menú
        if (_inputs != null)
        {
            _inputs.cursorLocked = false;
            _inputs.cursorInputForLook = false;
        }
    }

    public void Jugar()
    {
        // Assegura que el temps avança
        Time.timeScale = 1f;

        // Ocultar menús per estètica
        if (menuPrincipal != null) menuPrincipal.SetActive(false);
        if (menuOpciones != null) menuOpciones.SetActive(false);

        // Recuperar el nivell seleccionat al menú d’opcions
        string nivell = PlayerPrefs.GetString("NivellSeleccionat", "PrimeraPantallaJocUnity");
        Debug.Log("Càrregant nivell seleccionat: " + nivell);

        // Desa l’últim nivell per si es vol tornar-hi després
        PlayerPrefs.SetString("UltimNivell", nivell);
        PlayerPrefs.Save();

        // Càrrega de l’escena
        SceneManager.LoadScene(nivell);

        // Ajustar el cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Activar control del jugador
        if (_inputs == null)
            _inputs = Object.FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();

        if (_inputs != null)
        {
            _inputs.cursorLocked = true;
            _inputs.cursorInputForLook = true;
        }

#if ENABLE_INPUT_SYSTEM
        if (_playerInput == null)
            _playerInput = Object.FindFirstObjectByType<PlayerInput>();

        if (_playerInput != null && _playerInput.currentActionMap.name != "Player")
            _playerInput.SwitchCurrentActionMap("Player");
#endif
    }

    public void AbrirOpciones()
    {
        if (menuPrincipal != null) menuPrincipal.SetActive(false);
        if (menuOpciones != null) menuOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        if (menuOpciones != null) menuOpciones.SetActive(false);
        if (menuPrincipal != null) menuPrincipal.SetActive(true);
    }

    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
