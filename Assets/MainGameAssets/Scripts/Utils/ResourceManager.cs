using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    public static T LoadResouce<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    
    
}
