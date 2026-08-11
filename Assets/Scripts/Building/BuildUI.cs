using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : UserInterface
{
    static string scriptableObjectsDataPath = "ScriptableObject/BuildingData/SOBuildingData";
    private Dictionary<short, BuildingData> dataSlots = new Dictionary<short, BuildingData>();

    [SerializeField]
    private GameObject _slotImagePrefab;

    [SerializeField]
    private ScrollableWindow scrollWindow;
    
    private BuildingType sortType;

    private Button[] btns = new Button[(int)BuildingType.Count];
    private List<ISlot> slots = new List<ISlot>();


    protected override void Awake()
    {
        base.Awake();
        DataLoad();
    }
    private void OnEnable()
    {
        slots = scrollWindow.GetObjectsGeneric<ISlot>();
    }

    private void DataLoad()
    {
        SOBuildingData buildingData = Resources.Load<SOBuildingData>(scriptableObjectsDataPath);
        foreach (BuildingData data in buildingData.dataList)
        {
            dataSlots.Add(data.buildingID, data);
        }
    }

    public void OnClickSort(BuildingType type)
    {

    }
}
