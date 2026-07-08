using UnityEngine;

public class IdleHandPose : MonoBehaviour
{
    public Transform rightHandTarget;
    public Transform leftHandTarget;

    public Transform chest;   // Spine2 드래그

    public Vector3 rightOffset = new Vector3(0.12f, -0.02f, 0.15f);
    public Vector3 leftOffset  = new Vector3(-0.05f, 0.05f, 0.12f);

    void LateUpdate()
    {
        rightHandTarget.position =
            chest.TransformPoint(rightOffset);

        leftHandTarget.position =
            chest.TransformPoint(leftOffset);

        rightHandTarget.rotation = chest.rotation;
        leftHandTarget.rotation = chest.rotation;
    }
}