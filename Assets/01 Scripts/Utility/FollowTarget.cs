using UnityEditor;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [field: SerializeField]
    public Transform Target { get; private set; }
    [field: SerializeField]
    public Camera Camera { get; private set; }
    [SerializeField]
    private Vector3 Offset;

    public bool _debug = false;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {


        try
        {
            transform.position =  Camera.WorldToScreenPoint(Target.position) + Offset;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error following target: " + e.Message);
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(this);
#endif
        }
    }
}
