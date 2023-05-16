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

    public List<GameObject> rooms;
    public GameObject miner;

    private void Start()
    {
        Invoke("SpawnMiner", 3);
    }

    private void SpawnMiner()
    {
        Instantiate(miner, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
    }

    // Se elimina la sala si se intenta crear encima de la sala central
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpawnPoint")
        {
            Destroy(other.gameObject);
        }
    }

}
