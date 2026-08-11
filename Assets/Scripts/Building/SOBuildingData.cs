using System.Xml;
using UnityEngine;
using System.Collections.Generic;
using System;

public enum BuildingType
{
    None,
    Platform,
    Wall,
    Interactable,
    Count
}
[Serializable]
public struct BuildingData
{
    public BuildingType buildingType;
    public short buildingID;
    public Vector3 size;
    public string name;
    public string prefabPath;
}

[CreateAssetMenu(fileName = "SOBuildingData", menuName = "Scriptable Objects/BuildingData")]
public class SOBuildingData : ScriptableObject
{

    public List<BuildingData> dataList = new List<BuildingData>();
}
