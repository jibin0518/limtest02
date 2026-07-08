using UnityEngine;

public class IdleLegPose : MonoBehaviour
{
    public Transform hips; // Root 또는 골반 기준
    public Transform rightFootTarget;
    public Transform leftFootTarget;

    public Vector3 rightFootOffset = new Vector3(0.18f, -0.02f, 0.02f);
    public Vector3 leftFootOffset  = new Vector3(-0.18f, -0.02f, 0.02f);

    void LateUpdate()
    {
        rightFootTarget.position = hips.TransformPoint(rightFootOffset);
        leftFootTarget.position  = hips.TransformPoint(leftFootOffset);
    }
}