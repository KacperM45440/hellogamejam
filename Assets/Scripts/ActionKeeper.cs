using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionKeeper : MonoBehaviour
{
    public MenuScript menuRef;

    public void Level1Delay()
    {
        StartCoroutine(menuRef.WaitStart());
    }
}
