using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{

    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose";

    [SerializeField] private string player = "Player";

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Hello: " + other.name);

        //if (other.CompareTag(player))
        //{

        if (openTrigger)
        {
            myDoor.Play(doorOpen, 0, 0.0f);
            //gameObject.SetActive(false);
            openTrigger = false;
        }
        else
        {
            myDoor.Play(doorClose, 0, 0.0f);
            //gameObject.SetActive(false);
            openTrigger = true;
        }

        // }
    }
}
