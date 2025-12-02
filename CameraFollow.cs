using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 3.5f, -5f);
    public float cameraSpeed = 10f;

    void LateUpdate()
    {
        if (player == null) return;

        // Calcula la posición deseada RELATIVA a la rotación del jugador
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // Suaviza el movimiento de la cámara hacia esa posición
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);

        // Haz que la cámara mire hacia el jugador (ligeramente por encima del centro)
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
