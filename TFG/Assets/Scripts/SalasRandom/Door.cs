using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    /// <summary>
    /// Cuando hay una colisión se comprueba que ha sido con el jugador y que la puerta
    ///     es de la sala actual
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (other.tag == "Player" && GetPosicion(camera))
        {
            camera.transform.position += GetDirection();
        }
    }

    /// <summary>
    /// Se le resta a la posición de la puerta una distancia predeterminada para determinar si 
    ///     la puerta con la que hemos interactuado es de la sala que nos interesa (sala en la que nos hallamos)
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    private bool GetPosicion(Camera camera)
    {
        Vector3 aux = gameObject.tag switch
        {
            "Left" => new(-12.5f, 0, 10),
            "Top" => new(0, 5.5f, 10),
            "Right" => new(12.5f, 0, 10),
            "Bottom" => new(0, -5.5f, 10),
            _ => new(0, 0, 0),
        };
        return camera.transform.position - gameObject.transform.position + aux == new Vector3(0, 0, 0);
    }

    /// <summary>
    /// Devuelve hacia que dirección mover la cámara dependiendo
    ///     de la etiqueta asignada al objeto
    /// </summary>
    /// <returns></returns>
    private Vector3 GetDirection()
    {
        return gameObject.tag switch
        {
            "Left" => new(-24, 0, 0),
            "Top" => new(0, 10, 0),
            "Right" => new(24, 0, 0),
            "Bottom" => new(0, -10, 0),
            _ => new(0, 0, 0),
        };
    }
}
