using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int Level = 1;

    [SerializeField]
    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);

    }
}
