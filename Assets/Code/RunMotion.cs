using UnityEngine;

public class RunMotion : MonoBehaviour
{
    CharacterRigData rig;

    public float legForward = 0.28f;
    public float legUp = 0.18f;
    public float bodyBob = 0.01f;
    public float bodyLean = 3f;
    public float runSpeed = 4f;

    Vector3 rootStartPos;
    Vector3 spineStartPos;

    Quaternion spineStart;
    Quaternion spine1Start;
    Quaternion spine2Start;

    Vector3 rightFootBase;
    Vector3 leftFootBase;
    Vector3 rightHintBase;
    Vector3 leftHintBase;

    public RunMotion(CharacterRigData rig)
    {
        this.rig = rig;

        rootStartPos = rig.root.localPosition;
        spineStartPos = rig.spine.localPosition;

        spineStart = rig.spine.localRotation;
        spine1Start = rig.spine1.localRotation;
        spine2Start = rig.spine2.localRotation;

        rightFootBase = rig.rightFootTarget.position;
        leftFootBase = rig.leftFootTarget.position;

        rightHintBase = rig.rightFootHint.position;
        leftHintBase = rig.leftFootHint.position;
    }

    [Header("Run Hand Pose")]
    public Vector3 rightHandOffset = new Vector3(0.08f, -0.03f, 0.22f); // 가슴 위
    public Vector3 leftHandOffset  = new Vector3(-0.18f, -0.22f, 0.05f); // 주머니 쪽

    public Vector3 rightHandRot = new Vector3(0f, 0f, 0f);
    public Vector3 leftHandRot  = new Vector3(0f, 0f, 0f);

    public float handBobAmount = 0.015f;

    public void Play()
    {
        float phase = Time.time * runSpeed;

        float right = Mathf.Sin(phase);
        float left = Mathf.Sin(phase + Mathf.PI);

        float rightLift = Mathf.Max(0, right) * legUp;
        float leftLift = Mathf.Max(0, left) * legUp;

        rig.root.localPosition = rootStartPos;

        float smoothBob = Mathf.Sin(phase * 2f) * bodyBob;

        rig.spine.localPosition =
            spineStartPos + new Vector3(0, smoothBob, 0);

        rig.spine.localRotation =
            spineStart * Quaternion.Euler(bodyLean, 0, 0);

        rig.spine1.localRotation =
            spine1Start * Quaternion.Euler(bodyLean * 0.5f, 0, 0);

        rig.spine2.localRotation =
            spine2Start * Quaternion.Euler(bodyLean * 0.25f, 0, 0);

        rig.rightFootTarget.position =
            rightFootBase + rig.root.forward * (right * legForward) + Vector3.up * rightLift;

        rig.leftFootTarget.position =
            leftFootBase + rig.root.forward * (left * legForward) + Vector3.up * leftLift;

        rig.rightFootHint.position =
            rightHintBase + rig.root.forward * (right * 0.15f);

        rig.leftFootHint.position =
            leftHintBase + rig.root.forward * (left * 0.15f);

        float handBob = Mathf.Sin(phase * 2f) * handBobAmount;

        // 왼손: 주머니 쪽
        rig.leftHandTarget.position =
            rig.chest.TransformPoint(leftHandOffset + new Vector3(0, handBob-0.15f, 0));

        rig.rightHandTarget.rotation =
            rig.chest.rotation * Quaternion.Euler(leftHandRot);

        // 오른손: 가슴 위
        rig.rightHandTarget.position =
            rig.chest.TransformPoint(rightHandOffset + new Vector3(0, (handBob-0.2f) * 0.5f, -0.08f));

        rig.rightHandTarget.rotation =
            rig.chest.rotation * Quaternion.Euler(rightHandRot);
    }
}