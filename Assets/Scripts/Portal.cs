using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject linkedPortal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Puck"))
        {
           PuckScript puck = other.gameObject.GetComponent<PuckScript>(); 
            if (puck.canTeleport)
            {
                puck.transform.position = linkedPortal.transform.position;
                puck.Teleport();
            }
        }
    }

}