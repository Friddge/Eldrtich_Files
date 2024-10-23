using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime; 

   
    void Update()
    {
        if (isPickedUp)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
