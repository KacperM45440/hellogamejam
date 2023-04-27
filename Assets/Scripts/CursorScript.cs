using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D cursorArrow;
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //Debug.Log("Mouse Over: " + eventData.pointerCurrentRaycast.gameObject.name);
        }
        Cursor.visible = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.visible = true;
    }

}
