using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HTPmenu : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
