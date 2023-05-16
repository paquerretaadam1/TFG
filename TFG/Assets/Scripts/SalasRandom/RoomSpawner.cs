using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openSide;
    public float WaitTime = 0.1f;

    private RoomTemplates templates;
    private bool Spawned;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", WaitTime);
    }


    /// <summary>
    /// Cada punto de aparición (Spawnpoint) spawnea una habitación y asigna ese valor a true
    ///     si ya ha hecho aparecer una habitación se sale de la ejecución
    /// </summary>
    void Spawn()
    {
        if (Spawned)
        {
            return;
        }
        GameObject chosenRoom = GetChosenRoom();
        Instantiate(chosenRoom, transform.position, chosenRoom.transform.rotation);

        Spawned = true;
    }


    /// <summary>
    /// Busca en las listas una habitación con puerta en el lado deseado
    ///     y elige una de manera aleatoria y la devuelve
    /// </summary>
    /// <returns></returns>
    private GameObject GetChosenRoom()
    {
        GameObject[] neededRooms = { };
        switch (openSide)
        {
            // Necesita una PUERTA a la DERECHA
            case 0:
                neededRooms = templates.RigthRooms;
                break;

            // Necesita una PUERTA ABAJO
            case 1:
                neededRooms = templates.BottomRooms;
                break;

            // Necesita una PUERTA a la IZQUIERDA
            case 2:
                neededRooms = templates.LeftRooms;
                break;

            // Necesita una PUERTA ARRIBA
            case 3:
                neededRooms = templates.TopRooms;
                break;


            // No tiene puertas (ERROR)
            default:
                return null;
        }

        int random = Random.Range(0, neededRooms.Length);
        return neededRooms[random];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "SpawnPoint" && other.gameObject.tag != "Player")
        {
            print(other.gameObject.ToString());
        }
        if (other.CompareTag("SpawnPoint"))
        {
            if (!other.GetComponent<RoomSpawner>().Spawned && !Spawned && templates is not null)
            {
                Instantiate(GetChosenRoom(), transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Spawned = true;
        }
    }
}


