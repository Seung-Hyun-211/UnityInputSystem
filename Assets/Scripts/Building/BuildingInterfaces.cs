using System;
using UnityEngine;
public interface IBuildInfo
{
    public BuildingData Data { get; set; }
}

public interface ISlot
{
    public int SlotNum { get; }
    public void SetSlot(int num);
    public void SetImage(Texture2D texture);
    public void SetImage(Sprite sprite);
}