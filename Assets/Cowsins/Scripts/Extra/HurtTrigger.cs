using System.Collections;
using System.Collections.Generic;
using Cowsins.Player;
using UnityEngine;

public class HurtTrigger : MonoBehaviour
{
    [SerializeField] private float damage; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.GetComponent<PlayerStats>().Damage(damage); 
    }
}
