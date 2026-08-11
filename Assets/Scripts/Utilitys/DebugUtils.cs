using System.Diagnostics;

public static class DebugUtils
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log($"[EditorLog] {message}");
    }
}