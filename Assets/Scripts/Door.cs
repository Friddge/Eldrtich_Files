using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        locked = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            anim.SetTrigger("Open");
            locked = false;
        }
    }
}
   
