using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionKeeper : MonoBehaviour
{
    public MenuScript menuRef;
    public PlayerMovement movementRef;
    public bool boolLV1;
    private bool foundHim = false;
    public GameObject entranceRef;
    public void Level1Delay()
    {
        StartCoroutine(menuRef.WaitStart());
    }

    private void FixedUpdate()
    {
        if(PlayerReference.Instance.name.Equals("Player") && !foundHim)
        {
            foundHim = true;
            Level1Entry();
        }
    }

    public void Level1Entry()
    {
        movementRef = PlayerReference.Instance.playerMovement;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float duration = 2.5f;                   
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            movementRef.controller.Move(3f * Time.deltaTime * new Vector3(0, 0, 1));
            movementRef.movement.x = 1f;
            movementRef.playerBodyAnim.SetFloat("Z", Mathf.Lerp(movementRef.playerBodyAnim.GetFloat("Z"), 1f, Time.deltaTime * 10f));
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        entranceRef.SetActive(true);
    }


}
