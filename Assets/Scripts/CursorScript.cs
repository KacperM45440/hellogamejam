using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D cursorArrow;
    void Start()
    {
        //Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.visible = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.visible = true;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }
}
