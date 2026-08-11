using System;
using System.Collections.Generic;
using UnityEngine;
public class ScrollableWindow : MonoBehaviour
{
    [SerializeField]
    private RectOffset padding;
    [SerializeField]
    private Vector2 cellSize;
    private Vector2 PaddingCellSize => cellSize + new Vector2(padding.left + padding.right, padding.top + padding.bottom);



    [SerializeField]
    private float wheelValue;

    [SerializeField]
    private RectTransform maskingWindow;
    [SerializeField]
    private RectTransform movingTransform;
    private Vector2 windowSize = Vector2.zero;

    private int cellCount;
    private int windowCellCount;

    private int cellWidthCount;
    [SerializeField]
    private GameObject cellObj;
    private List<GameObject> objPool = new List<GameObject>();

    private float minPosY;
    private float maxPosY;
    private int prevLevel;

    bool dirty = false;


    private void Awake()
    {
        //TestValue
        windowCellCount = 5;
        wheelValue = 10;


        Initialize();
    }
    private void OnEnable()
    {
        PlayerInputReader.Instance.OnWheelInput += OnWheel;
        if (dirty)
        {
            MovingWindowSetting();
        }
    }
    private void OnDisable()
    {
        //PlayerInputReader.Instance.OnWheelInput -= OnWheel;

    }

    public List<T> GetObjectsGeneric<T>() where T: class
    {
        List<T> resultList = new List<T>();

        foreach (GameObject obj in objPool)
        {
            if (obj != null)
            {
                resultList.Add(obj.GetComponent<T>());
            }
        }

        return resultList;
    }



    private void Initialize()
    {
        CellCalculate();
        ObjectPoolSetting();

        dirty = true;
    }
    private void CellCalculate()
    {
        windowSize = maskingWindow.rect.size;
        cellWidthCount = Mathf.FloorToInt(windowSize.x / PaddingCellSize.x);

        cellCount = cellWidthCount * (2 + Mathf.FloorToInt(windowSize.y / PaddingCellSize.y));
    }
    private void ObjectPoolSetting()
    {
        int max = cellCount < windowCellCount ? cellCount : windowCellCount;
        for (int i = 0; i < max; i++)
        {
            GameObject obj = Instantiate(cellObj, movingTransform);
            obj.GetComponent<RectTransform>().sizeDelta = cellSize;
            objPool.Add(obj);
        }

    }

    private void MovingWindowSetting()
    {
        movingTransform.sizeDelta = new Vector2(cellWidthCount * PaddingCellSize.x,
            Math.Max(windowSize.y, Mathf.CeilToInt(windowCellCount / cellWidthCount) * PaddingCellSize.y));

        maxPosY = (movingTransform.rect.height - maskingWindow.rect.height) / 2;
        minPosY = -maxPosY;

        for (int i = 0; i < objPool.Count; i++)
        {
            objPool[i].GetComponent<RectTransform>().anchoredPosition = IndexToPosition(i);
        }

        movingTransform.localPosition = new Vector2(0, minPosY);
        prevLevel = 0;

    }
    private void OnWheel(float input)
    {
        float newPosY = Mathf.Clamp(movingTransform.anchoredPosition.y + wheelValue * -input, minPosY, maxPosY);
        movingTransform.localPosition = new Vector2(0, newPosY);

        OnPositionChanged(newPosY);

    }
    
private void OnPositionChanged(float position)
    {
        int level = Mathf.FloorToInt((position - minPosY) / PaddingCellSize.y);
        Debug.Log("Level = " + level);

        if (prevLevel < level)
        {
            for (int i = 0; i < cellWidthCount; i++)
            {
                int prevIndex = prevLevel * cellWidthCount + i;
                
                if (prevIndex + cellCount >= windowCellCount)
                { 
                    break;
                }
                int poolIndex = prevIndex % cellCount;
                
                objPool[poolIndex].GetComponent<RectTransform>().anchoredPosition = IndexToPosition(prevIndex + cellCount);
                //여기서 보여줄 아이템을 설정할 Index -> prevIndex+cellCount
            }
            prevLevel = level;
        }
        else if (prevLevel > level)
        {
            for (int i = 0; i < cellWidthCount; i++)
            {
                int targetIndex = level * cellWidthCount + i;
                
                if (targetIndex < 0)
                {
                    break;
                }
                int poolIndex = targetIndex % cellCount;

                objPool[poolIndex].GetComponent<RectTransform>().anchoredPosition = IndexToPosition(targetIndex);
                //여기서 보여줄 아이템을 설정할 Index -> targetIndex
            }
            prevLevel = level;
        }
    }
    private Vector2 IndexToPosition(int index)
    {
        return new Vector2(((index % cellWidthCount) + 0.5f) * PaddingCellSize.x, -((index / cellWidthCount) + 0.5f) * PaddingCellSize.y);
    }

    public void SetObjectCount(int count)
    {



        MovingWindowSetting();
    }
    public void SetDirty()
    {
        dirty = true;
    }

}
