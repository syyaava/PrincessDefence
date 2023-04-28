using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public ResourceType Type;
    public int Count = 0;

    public enum ResourceType
    {
        Score,
        Gold
    }
}
