using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomToList : MonoBehaviour
{
    private RoomTemplates templates;
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(gameObject);
    }
}
