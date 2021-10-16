using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPair : MonoBehaviour
{
    private List<PortalObject> portals = new List<PortalObject>();
    PortalObject portalA;
    PortalObject portalB;
    List<GameObject> trackedTargets = new List<GameObject>();
    Dictionary<GameObject, PortalObject> travelBook = new Dictionary<GameObject, PortalObject>();
    public void Subsribe(PortalObject portal)
    {
        if (!portals.Contains(portal))
        {
            portals.Add(portal);
        }

        if (portals.Count >= 2)
        {
            SortPortals();
        }
    }

    void SortPortals()
    {
        portalA = portals[0];
        portalB = portals[1];
    }

    public void Teleport(PortalObject sourcePortal, GameObject target)
    {
        if (!isTrackedTraveller(target))
        {
            trackedTargets.Add(target);

            if (sourcePortal == portalA)
            {
                travelBook.Add(target, portalA);
                Vector3 targetPosition = portalB.teleportPosition.position;
                if (sourcePortal.type == PortalType.Horizontal)
                {

                    float targetVar = target.transform.position.y;
                    targetPosition.y = targetVar;


                }
                else
                {
                    float targetVar = target.transform.position.x;
                    targetPosition.x = targetVar;
                }
                target.transform.position = targetPosition;
            }
            else
            {
                travelBook.Add(target, portalB);
                Vector3 targetPosition = portalA.teleportPosition.position;
                if (sourcePortal.type == PortalType.Horizontal)
                {
                    
                    float targetVar = target.transform.position.y;
                    targetPosition.y = targetVar;

                    
                }
                else
                {
                    float targetVar = target.transform.position.x;
                    targetPosition.x = targetVar;
                }
                target.transform.position = targetPosition;

            }
        }
    }

    public bool isTrackedTraveller(GameObject target)
    { 
        if (trackedTargets.Contains(target))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TryToRemoveTraveller(PortalObject sourcePortal, GameObject target)
    {
        if (isTrackedTraveller(target))
        {
            if (travelBook.ContainsKey(target) && sourcePortal != travelBook[target])
            {
                travelBook.Remove(target);
                trackedTargets.Remove(target);
            }
        }
      
    }
}
