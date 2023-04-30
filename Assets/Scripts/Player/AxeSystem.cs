using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSystem : MonoBehaviour
{
    [SerializeField] private GameObject axe;

    public void EnableAxe() {
        axe.SetActive(true);
    }

    public void DisableAxe()
    {
        axe.SetActive(false);
    }
}
