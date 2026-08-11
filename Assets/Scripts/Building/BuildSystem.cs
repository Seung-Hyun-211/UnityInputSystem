using System.Collections;
using TMPro;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField]
    private Material matTransparent;
    [SerializeField]
    private Material matBuild;

    private UserInterface buildInterface;


    private bool isBuildMode;
    private bool isMagnetic;
    
    [SerializeField]
    private GameObject currentObj;
    private float objRotateY;

    private IBuildInfo info;

    //private Vector3 NORMAL_ROTATE = new Vector3(0f, 15f, 0f);
    //private Vector3 SMALL_ROTATE = new Vector3(0f, 5f, 0f);

    const float NORMAL_ROTATE = 15f;
    const float SMALL_ROTATE = 5f;

    private IInputAlt alt;

    

    private void Awake()
    {
        if (currentObj == null)
        {
            currentObj = new GameObject("CurrentObj");
            currentObj.transform.parent = this.transform;
        }
        else if (currentObj.transform.parent != this.transform)
        {
            currentObj.transform.parent = this.transform;
        }
        objRotateY = 0f;
    }

    private void OnEnable()
    {
        isBuildMode = false;
        
        if (currentObj == null)
        {
            currentObj = new GameObject("CurrentObj");
            currentObj.transform.parent = this.transform;
        }

        info = null;
        
        BindingEvent();
        StartCoroutine(WaitBuildUI());
    }

    private void ToggleBuildMode()
    {
        isBuildMode = !isBuildMode;
        buildInterface.Toggle(isBuildMode);


        if (isBuildMode)
        {
            objRotateY = 0f;
            currentObj.transform.rotation = Quaternion.Euler(Vector3.zero);
            //currentObj.SetActive(true);
        }
        else
        {
            //currentObj.SetActive(false);
        }
    }

    private void WheelInput(float value)
    {
        if (isBuildMode || info!=null)
        {
            objRotateY += value * (alt.Alt ? SMALL_ROTATE : NORMAL_ROTATE);
            currentObj.transform.rotation = Quaternion.Euler(0f, objRotateY, 0f);


            Debug.Log($"Rotate obj : {currentObj.transform.rotation.y}");
        }
    }
    private void Confirm()
    {
        if (isBuildMode)
        {

        }
    }
    private void SelectObj()
    {

    }

    private void BindingEvent()
    {
        var _input = PlayerInputReader.Instance;
        alt = _input;

        _input.OnBuildInput += ToggleBuildMode;
        _input.OnWheelInput += WheelInput;
        _input.OnConfirmInput += Confirm;

    }

    private IEnumerator WaitBuildUI()
    {
        yield return new WaitUntil(() => UIController.Instance.IsInitialized(UIType.Build));
        buildInterface = UIController.Instance.GetUserInterface(UIType.Build);
    }
}
