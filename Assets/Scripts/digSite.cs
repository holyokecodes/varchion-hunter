using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;

[System.Serializable]
public class DigSite
{
    public Vector2d latLong;

    public int treasureID;
    public enum stateTypes
    {
        future,
        current,
        past
    }
    public stateTypes state = stateTypes.current;
}
