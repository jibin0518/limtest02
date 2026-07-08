using UnityEngine;

public class IdleBodyMotion : MonoBehaviour
{
    public Transform spine;
    public Transform spine1;
    public Transform spine2;

    public float speed = 1.5f;
    public float bodyAngle = 4f;

    Quaternion spineStart;
    Quaternion spine1Start;
    Quaternion spine2Start;

    void Start()
    {
        spineStart = spine.localRotation;
        spine1Start = spine1.localRotation;
        spine2Start = spine2.localRotation;
    }

    void LateUpdate()
    {
        float t = Mathf.Sin(Time.time * speed);

        spine.localRotation = spineStart * Quaternion.Euler(t * bodyAngle, 0, 0);
        spine1.localRotation = spine1Start * Quaternion.Euler(t * bodyAngle * 0.5f, 0, 0);
        spine2.localRotation = spine2Start * Quaternion.Euler(t * bodyAngle * 0.3f, 0, 0);
    }
}