using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    bool isBuildMode;
    GameObject currentObj;



    private void Awake()
    {
    }



    void ToggleBuildMode()
    {
        isBuildMode = !isBuildMode;



    }

    void Confirm()
    {
        if (isBuildMode)
        {




        }
    }
}
