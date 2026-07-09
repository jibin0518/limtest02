using UnityEngine;

public class IdleMotion
{
    CharacterRigData rig;

    public float idleSpeed = 1.5f;
    public float bodyAngle = 1.5f;
    public float bodyUpDown = 0.03f;
    public float handBobAmount = 0.015f;

    public Vector3 rightHandOffset = new Vector3(0.0f, -0.08f, 0.24f);
    public Vector3 leftHandOffset = new Vector3(-0.05f, -0.05f, 0.18f);

    public Vector3 rightFootOffset = new Vector3(0.2f, -0.795f, 0.02f);
    public Vector3 leftFootOffset = new Vector3(-0.2f, -0.795f, 0.02f);

    Quaternion spineStart;
    Quaternion spine1Start;
    Quaternion spine2Start;

    Vector3 rootStartPos;
    Vector3 spineStartPos;

    Vector3 rightFootWorldStart;
    Vector3 leftFootWorldStart;
    Vector3 rightFootHintWorldStart;
    Vector3 leftFootHintWorldStart;

    public IdleMotion(CharacterRigData rig)
    {
        this.rig = rig;

        rootStartPos = rig.root.localPosition;
        spineStartPos = rig.spine.localPosition;

        spineStart = rig.spine.localRotation;
        spine1Start = rig.spine1.localRotation;
        spine2Start = rig.spine2.localRotation;

        rig.rightFootTarget.position = rig.root.TransformPoint(rightFootOffset);
        rig.leftFootTarget.position = rig.root.TransformPoint(leftFootOffset);

        rightFootWorldStart = rig.rightFootTarget.position;
        leftFootWorldStart = rig.leftFootTarget.position;
        rightFootHintWorldStart = rig.rightFootHint.position;
        leftFootHintWorldStart = rig.leftFootHint.position;
    }

    public void Play()
    {
        float t = Mathf.Sin(Time.time * idleSpeed);

        rig.root.localPosition = rootStartPos;
        rig.spine.localPosition = spineStartPos + new Vector3(0, t * bodyUpDown, 0);

        rig.spine.localRotation = spineStart * Quaternion.Euler(t * bodyAngle, 0, 0);
        rig.spine1.localRotation = spine1Start * Quaternion.Euler(t * bodyAngle * 0.5f, 0, 0);
        rig.spine2.localRotation = spine2Start * Quaternion.Euler(t * bodyAngle * 0.3f, 0, 0);

        float handBob = t * handBobAmount;

        rig.rightHandTarget.position =
            rig.chest.TransformPoint(rightHandOffset + new Vector3(0, handBob, 0));

        rig.leftHandTarget.position =
            rig.chest.TransformPoint(leftHandOffset + new Vector3(0, handBob, 0));

        rig.rightHandTarget.rotation = rig.chest.rotation;
        rig.leftHandTarget.rotation = rig.chest.rotation;

        rig.rightFootTarget.position = rightFootWorldStart;
        rig.leftFootTarget.position = leftFootWorldStart;
        rig.rightFootHint.position = rightFootHintWorldStart;
        rig.leftFootHint.position = leftFootHintWorldStart;
    }
}