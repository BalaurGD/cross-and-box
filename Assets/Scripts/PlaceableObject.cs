using NaughtyAttributes;
using System;


[Serializable]
public class PlaceableObject 
{
    public int XCordinat;
    public int ZCordinat;
    [Dropdown("ObjectNameAvailable")] public Object ObjectName;
    private Object[] ObjectNameAvailable = new Object[] {
        Object.Red, 
        Object.Green, 
        Object.Conveyor,
        Object.Saw};
}
