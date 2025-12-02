using UnityEngine;

public class FinalPartidaTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider other)
    {
       if (!other.CompareTag("Player")) return;

        Debug.Log("Jugador ha tocat la ximenea… comprovant llenya…");

        // Llenya necessària per guanyar
        int recollida = GameManager.instance.llenyaRecollida;
        int total = GameManager.instance.llenyaTotal;

        // Si tens tota la llenya guanyes
        if (recollida >= total)
        {
            Debug.Log("TOTA LA LLENYA RECULLIDA. HAS GUANYAT LA PARTIDA");
            GameManager.instance.GuanyarPartida();
        }
        else
        {
            Debug.Log("FALTA LLENYA. HAS PERDUT LA PARTIDA");
            GameManager.instance.PerdrePartida();

        }
    }

}
