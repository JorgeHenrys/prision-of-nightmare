﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
