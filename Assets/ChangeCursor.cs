using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorimg;
    void Start()
    {
        Cursor.SetCursor(cursorimg, new Vector2(75,75), CursorMode.ForceSoftware);
    }
}
