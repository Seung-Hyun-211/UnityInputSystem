using UnityEngine;
public interface IInputMove
{
    Vector2 Direction { get; }
    bool Sprint { get; }
}

public interface IInputCamera
{
    Vector2 CameraInput { get; }
}

public interface IInputWheel
{
    float Wheel { get; }
}


public interface IInputAlt
{
    bool Alt { get; }
}
