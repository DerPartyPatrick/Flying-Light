using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static string controller = "";
    public static bool usingController = controller=="PS4" || controller=="XBOX"; 

    public static float PrimaryDirection_Horizontal()
    {
        float horizontal = 0f;
        horizontal += Input.GetAxis("KB_PrimaryDir_Horizontal");

        if(controller == "PS4")
        {
            horizontal += Input.GetAxis("DS_PrimaryDir_Horizontal");
        }
        else if(controller == "XBOX")
        {
            horizontal += Input.GetAxis("XB_PrimaryDir_Horizontal");
        }

        horizontal = Mathf.Clamp(horizontal, -1f, 1f); 
        return horizontal; 
    }

    public static float PrimaryDirection_Vertical()
    {
        float vertical = 0f;
        vertical += Input.GetAxis("KB_PrimaryDir_Vertical");
        
        if(controller == "PS4")
        {
            vertical += Input.GetAxis("DS_PrimaryDir_Vertical");
        }
        else if(controller == "XBOX")
        {
            vertical += Input.GetAxis("XB_PrimaryDir_Vertical");
        }

        vertical = Mathf.Clamp(vertical, -1f, 1f); 
        return vertical; 
    }

    public static float SecondaryDirection_Horizontal()
    {
        float horizontal = 0f;
        
        if(controller == "PS4")
        {
            horizontal += Input.GetAxis("DS_SecondaryDir_Horizontal");
        }
        else if(controller == "XBOX")
        {
            horizontal += Input.GetAxis("XB_SecondaryDir_Horizontal"); 
           
        }

        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        Debug.Log("hotizontal:" + horizontal);
        return horizontal; 
    }

    public static float SecondaryDirection_Vertical()
    {
        float vertical = 0f;
        
        if(controller == "PS4")
        {
            vertical += Input.GetAxis("DS_SecondaryDir_Vertical");
        }
        else if(controller == "XBOX")
        {
            vertical += Input.GetAxis("XB_SecondaryDir_Vertical"); 
        }

        vertical = Mathf.Clamp(vertical, -1f, 1f);
        Debug.Log("vertical:" + vertical);
        return vertical; 
    }

    public static bool Fire()
    {
        bool pressed = false;

        if(controller == "PS4")
        {
            if(Input.GetAxis("PS4_Fire") < 0)
            {
                pressed = true;
            }
        }
        else if(controller == "XBOX")
        {
            if(Input.GetAxis("XBOX_Fire") <  0)
            {
                pressed = true; 
            }
        }

        pressed = Input.GetButtonDown("MouseFire") || pressed;
        return pressed; 
    }

    public static bool Pause()
    {
        bool pressed = false;

        if(controller == "PS4")
        {
            pressed = Input.GetButtonDown("DS_Pause") || pressed; 
        }
        else if(controller == "XBOX")
        {
            pressed = Input.GetButtonDown("XB_Pause") || pressed; 
        }

        pressed = Input.GetButtonDown("KB_Pause") || pressed;
        return pressed;
    }

    public static Vector2 MousePosition()
    {
        Vector3 mp = Input.mousePosition;
        mp = Camera.main.ScreenToWorldPoint(mp);
        return mp; 
    }
}
