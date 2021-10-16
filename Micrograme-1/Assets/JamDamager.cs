using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public PlayerController controller;
    public bool isTongue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTongue)
        {
            if (collision.gameObject.CompareTag("damageReceiver"))
            {
                PlayerController target = collision.gameObject.GetComponentInParent<PlayerController>();
                if (target != null && target != controller)
                {
                    target.TakeDamage();
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController target = collision.gameObject.GetComponent<PlayerController>();
                if (target != null && target != controller)
                {
                    target.TakeDamage();
                }
            }
        }
       
    }
}
