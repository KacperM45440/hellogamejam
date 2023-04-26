using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{



    public void ExitGame()
    {
        SceneManager.LoadScene(-1);
    }
} 