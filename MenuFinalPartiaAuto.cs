using UnityEngine;
using System.Collections;

public class MenuFinalPartidaAuto : MonoBehaviour
{
    IEnumerator Start()
    {
        // Esperar hasta que exista un GameManager
        while (GameManager.instance == null)
        {
            yield return null;
        }

        // Registrar el menú
        GameManager.instance.menuFinalPartida = gameObject;
        Debug.Log("MenuFinalPartida registrado automáticamente: " + gameObject.name);
    }
}
