using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : MonoBehaviour
{
    public PortalType type;
    PortalPair parent;
    public Transform teleportPosition;
    private void Start()
    {
        teleportPosition = this.transform;
        parent = this.GetComponentInParent<PortalPair>();
        if (parent != null)
        {
            parent.Subsribe(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent.Teleport(this, collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent.TryToRemoveTraveller(this, collision.gameObject);
        }
    }
}


