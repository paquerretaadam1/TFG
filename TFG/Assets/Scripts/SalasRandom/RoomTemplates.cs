using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //Listas con habitaciones que tienen una puerta en el lado indicado 

    public GameObject[] LeftRooms = { };
    public GameObject[] TopRooms = { };
    public GameObject[] RigthRooms = { };
    public GameObject[] BottomRooms = { };


    // Se elimina la sala si se intenta crear encima de la sala central
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpawnPoint")
        {
            Destroy(other.gameObject);
        }
    }
}
