using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TipScript : MonoBehaviour
{
    public List<string> allTips;
    string level1 = "Order is the highest of virtues; a pillar of society.";
    string level2 = "It is never too late to take a step back.";
    string level3 = "All of the world's wisdom comes from the people before you.";
    string final = "Hope has not abandoned you.";

    public int whichScene;
    public TMP_Text tipTextRef;

    private void Start()
    {
        allTips.Add(level1);
        allTips.Add(level2);
        allTips.Add(level3);
        allTips.Add(final);

        whichScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        LoadTip();
    }
    public void LoadTip()
    {
        tipTextRef.text = allTips[whichScene-1];
    }    
}
