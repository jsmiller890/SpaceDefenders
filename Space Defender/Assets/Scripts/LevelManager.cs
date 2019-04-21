using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name)
    {
        Debug.Log("Load level requested for " + name);
        Application.LoadLevel(name);
    }
    public void QuitLevel()
    {
        Debug.Log("Quit requested for " + name);
        Application.Quit();
    }

}
