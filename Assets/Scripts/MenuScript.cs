using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour

{
    [SerializeField] GameObject BackGround;


    public void About()
    {
        BackGround.transform.position = new Vector2(42f, 360f);
    }


}
