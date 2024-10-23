using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
            EndGame();
        }


       
    }
    public void EndGame()
    {
        SceneManager.LoadSceneAsync(3);
    }

}
