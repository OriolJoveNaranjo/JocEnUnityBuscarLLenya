using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public AudioClip soRecollida;
    public int value = 1;

    private bool recollit = false;  // evita doble col·lisió

    private void OnTriggerEnter(Collider other)
    {       
        if (recollit) return;  // si ja s'ha recollit un cop, no sumar més

        if (other.CompareTag("Player"))
        {
            recollit = true;  // bloqueja execucions duplicades

            // Afegir llenya al GameManager
            if (GameManager.instance != null)
                GameManager.instance.AfegirLlenya(value);

            // Reproduir so
            if (soRecollida != null)
                AudioSource.PlayClipAtPoint(soRecollida, transform.position);

            // Eliminar objecte
            Destroy(gameObject);
        }
        Debug.Log("Collider detectado: " + other.name);
    }
}

