using UnityEngine;

public class InteractableMap : InteractableObject
{
    [SerializeField] private Animator animatorRef;
    private bool mapOpened = false;

    public override void Interact()
    {
        if (mapOpened)
        { 
            return; 
        }
        animatorRef.gameObject.SetActive(true);
        animatorRef.SetTrigger("Appear");
        mapOpened = true;
    }

    public new void Update()
    {
        base.Update();

        try
        {
            animatorRef = MapReference.Instance.animatorRef;
        }
        catch
        {

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!mapOpened)
            {
                return;
            }
            animatorRef.SetTrigger("DismissND");
            mapOpened = false;
        }
    }
}
