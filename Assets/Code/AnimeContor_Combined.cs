using UnityEngine;

// File name and class name must match in Unity.
// Save this file as: AnimeContor_Combined.cs
public class AnimeContor_Combined : MonoBehaviour
{
    public enum AnimState
    {
        Idle,
        Walk,
        Run,
        Attack,
        Hit,
        Death,
        Dash
    }

    [Header("State")]
    public AnimState currentState = AnimState.Idle;

    private CharacterRigData rig;

    [Header("Idle Settings")]
    public float idleSpeed = 1.5f;
    public float idleBodyAngle = 1.5f;
    public float idleBodyUpDown = 0.03f;
    public float idleHandBobAmount = 0.015f;

    public Vector3 idleRightHandOffset = new Vector3(0.0f, -0.08f, 0.24f);
    public Vector3 idleLeftHandOffset = new Vector3(-0.05f, -0.05f, 0.18f);

    public Vector3 idleRightFootOffset = new Vector3(0.2f, -0.795f, 0.02f);
    public Vector3 idleLeftFootOffset = new Vector3(-0.2f, -0.795f, 0.02f);

    [Header("Run Settings")]
    public float legForward = 0.28f;
    public float legUp = 0.18f;
    public float bodyBob = 0.01f;
    public float bodyLean = 3f;
    public float runSpeed = 4f;

    [Header("Run Foot Pose")]
    public float runFootSideDistance = 0.12f;
    public float runFootHeight = -0.795f;
    public float runFootZ = 0.02f;

    [Header("Run Hand Pose")]
    public Vector3 runRightHandOffset = new Vector3(0.08f, -0.03f, 0.3f);
    public Vector3 runLeftHandOffset = new Vector3(-0.18f, -0.22f, 0.05f);

    public Vector3 runRightHandRot = new Vector3(0f, 0f, 0f);
    public Vector3 runLeftHandRot = new Vector3(0f, 0f, 0f);

    public float runHandBobAmount = 0.015f;

[Header("Dash Settings")]
public float dashDuration = 0.18f;
public float dashPrepareRatio = 0.35f;

public float dashMoveDistance = 2.0f;

private float dashTimer;
private bool isDashing;
private Vector3 dashStartWorldPos;
private Vector3 dashEndWorldPos;

    [Header("Dash Prepare Pose")]
    public float dashPrepareLean = 40f;
    public float dashPrepareDown = 0.08f;

    public Vector3 dashPrepareRightHand = new Vector3(0.03f, -0.02f, 0.22f);
    public Vector3 dashPrepareLeftHand = new Vector3(-0.18f, -0.15f, -0.18f);

    public Vector3 dashPrepareRightFoot = new Vector3(0.08f, -0.80f, 0.15f);
    public Vector3 dashPrepareLeftFoot = new Vector3(-0.08f, -0.80f, -0.35f);

    [Header("Dash Launch Pose")]
    public float dashLaunchLean = 55f;
    public float dashLaunchDown = 0.04f;

    public Vector3 dashLaunchRightHand = new Vector3(0.12f, -0.05f, 0.35f);
    public Vector3 dashLaunchLeftHand = new Vector3(-0.22f, -0.12f, -0.28f);

    public Vector3 dashLaunchRightFoot = new Vector3(0.05f, -0.78f, 0.45f);
    public Vector3 dashLaunchLeftFoot = new Vector3(-0.03f, -0.80f, -0.05f);

    private Quaternion spineStart;
    private Quaternion spine1Start;
    private Quaternion spine2Start;

    private Vector3 rootStartPos;
    private Vector3 spineStartPos;

    private Vector3 idleRightFootWorldStart;
    private Vector3 idleLeftFootWorldStart;
    private Vector3 idleRightFootHintWorldStart;
    private Vector3 idleLeftFootHintWorldStart;

    private Vector3 runRightFootBase;
    private Vector3 runLeftFootBase;
    private Vector3 runRightHintBase;
    private Vector3 runLeftHintBase;

    private void Awake()
    {
        rig = new CharacterRigData(transform);
        CacheStartPose();
    }

    private void CacheStartPose()
    {
        rootStartPos = rig.root.localPosition;
        spineStartPos = rig.spine.localPosition;

        spineStart = rig.spine.localRotation;
        spine1Start = rig.spine1.localRotation;
        spine2Start = rig.spine2.localRotation;

        rig.rightFootTarget.position = rig.root.TransformPoint(idleRightFootOffset);
        rig.leftFootTarget.position = rig.root.TransformPoint(idleLeftFootOffset);

        idleRightFootWorldStart = rig.rightFootTarget.position;
        idleLeftFootWorldStart = rig.leftFootTarget.position;
        idleRightFootHintWorldStart = rig.rightFootHint.position;
        idleLeftFootHintWorldStart = rig.leftFootHint.position;

        runRightFootBase = rig.root.TransformPoint(new Vector3(runFootSideDistance, runFootHeight, runFootZ));
        runLeftFootBase = rig.root.TransformPoint(new Vector3(-runFootSideDistance, runFootHeight, runFootZ));
        runRightHintBase = rig.rightFootHint.position;
        runLeftHintBase = rig.leftFootHint.position;
    }

    private void LateUpdate()
    {
        if (rig == null)
        {
            return;
        }

        switch (currentState)
        {
            case AnimState.Idle:
                PlayIdle();
                break;

            case AnimState.Run:
                PlayRun();
                break;
            case AnimState.Dash:
                PlayDash();
                break;    
        }
    }

    private void PlayIdle()
    {
        float t = Mathf.Sin(Time.time * idleSpeed);

        rig.root.localPosition = rootStartPos;
        rig.spine.localPosition = spineStartPos + new Vector3(0f, t * idleBodyUpDown, 0f);

        rig.spine.localRotation = spineStart * Quaternion.Euler(t * idleBodyAngle, 0f, 0f);
        rig.spine1.localRotation = spine1Start * Quaternion.Euler(t * idleBodyAngle * 0.5f, 0f, 0f);
        rig.spine2.localRotation = spine2Start * Quaternion.Euler(t * idleBodyAngle * 0.3f, 0f, 0f);

        float handBob = t * idleHandBobAmount;

        rig.rightHandTarget.position =
            rig.chest.TransformPoint(idleRightHandOffset + new Vector3(0f, handBob, 0f));

        rig.leftHandTarget.position =
            rig.chest.TransformPoint(idleLeftHandOffset + new Vector3(0f, handBob, 0f));

        rig.rightHandTarget.rotation = rig.chest.rotation;
        rig.leftHandTarget.rotation = rig.chest.rotation;

        rig.rightFootTarget.position = idleRightFootWorldStart;
        rig.leftFootTarget.position = idleLeftFootWorldStart;
        rig.rightFootHint.position = idleRightFootHintWorldStart;
        rig.leftFootHint.position = idleLeftFootHintWorldStart;
    }

    private void PlayRun()
    {
        float phase = Time.time * runSpeed;

        float right = Mathf.Sin(phase);
        float left = Mathf.Sin(phase + Mathf.PI);

        float rightLift = Mathf.Max(0f, right) * legUp;
        float leftLift = Mathf.Max(0f, left) * legUp;

        rig.root.localPosition = rootStartPos;

        float smoothBob = Mathf.Sin(phase * 2f) * bodyBob;
        rig.spine.localPosition = spineStartPos + new Vector3(0f, smoothBob, 0f);

        rig.spine.localRotation = spineStart * Quaternion.Euler(bodyLean, 0f, 0f);
        rig.spine1.localRotation = spine1Start * Quaternion.Euler(bodyLean * 0.5f, 0f, 0f);
        rig.spine2.localRotation = spine2Start * Quaternion.Euler(bodyLean * 0.25f, 0f, 0f);

        rig.rightFootTarget.position =
            runRightFootBase + rig.root.forward * (right * legForward) + Vector3.up * rightLift;

        rig.leftFootTarget.position =
            runLeftFootBase + rig.root.forward * (left * legForward) + Vector3.up * leftLift;

        rig.rightFootHint.position =
            runRightHintBase + rig.root.forward * (right * 0.15f);

        rig.leftFootHint.position =
            runLeftHintBase + rig.root.forward * (left * 0.15f);

        float handBob = Mathf.Sin(phase * 2f) * runHandBobAmount;

        rig.leftHandTarget.position =
            rig.chest.TransformPoint(runLeftHandOffset + new Vector3(0f, handBob - 0.18f, 0f));

        rig.leftHandTarget.rotation =
            rig.chest.rotation * Quaternion.Euler(runLeftHandRot);

        rig.rightHandTarget.position =
            rig.chest.TransformPoint(runRightHandOffset + new Vector3(0f, (handBob - 0.2f) * 0.5f, -0.1f));

        rig.rightHandTarget.rotation =
            rig.chest.rotation * Quaternion.Euler(runRightHandRot);
    }

    public void StartDash()
    {
    currentState = AnimState.Dash;

    dashTimer = 0f;
    isDashing = true;

    dashStartWorldPos = transform.position;
    dashEndWorldPos = transform.position + transform.forward * dashMoveDistance;
    }

    private void PlayDash()
    {
        if (!isDashing)
        {
            StartDash();
        }

        dashTimer += Time.deltaTime;

        float t = Mathf.Clamp01(dashTimer / dashDuration);

        float moveT = Mathf.SmoothStep(0f, 1f, t);
        transform.position = Vector3.Lerp(dashStartWorldPos, dashEndWorldPos, moveT);

        float poseT;

        if (t < dashPrepareRatio)
        {
            poseT = 0f;
        }
        else
        {
            poseT = Mathf.InverseLerp(dashPrepareRatio, 1f, t);
            poseT = Mathf.SmoothStep(0f, 1f, poseT);
        }

        float bodyLean = Mathf.Lerp(dashPrepareLean, dashLaunchLean, poseT);
        float bodyDown = Mathf.Lerp(dashPrepareDown, dashLaunchDown, poseT);

        Vector3 rightHand = Vector3.Lerp(dashPrepareRightHand, dashLaunchRightHand, poseT);
        Vector3 leftHand = Vector3.Lerp(dashPrepareLeftHand, dashLaunchLeftHand, poseT);

        Vector3 rightFoot = Vector3.Lerp(dashPrepareRightFoot, dashLaunchRightFoot, poseT);
        Vector3 leftFoot = Vector3.Lerp(dashPrepareLeftFoot, dashLaunchLeftFoot, poseT);

        rig.root.localPosition = rootStartPos + Vector3.down * bodyDown;

        rig.spine.localPosition = spineStartPos;

        rig.spine.localRotation =
            spineStart * Quaternion.Euler(bodyLean, 0f, 0f);

        rig.spine1.localRotation =
            spine1Start * Quaternion.Euler(bodyLean * 0.6f, 0f, 0f);

        rig.spine2.localRotation =
            spine2Start * Quaternion.Euler(bodyLean * 0.3f, 0f, 0f);

        rig.rightHandTarget.position =
            rig.chest.TransformPoint(rightHand);

        rig.leftHandTarget.position =
            rig.chest.TransformPoint(leftHand);

        rig.rightHandTarget.rotation = rig.chest.rotation;
        rig.leftHandTarget.rotation = rig.chest.rotation;

        rig.rightFootTarget.position =
            rig.root.TransformPoint(rightFoot);

        rig.leftFootTarget.position =
            rig.root.TransformPoint(leftFoot);

        rig.rightFootHint.position =
            rig.root.TransformPoint(new Vector3(0.10f, -0.45f, 0.15f));

        rig.leftFootHint.position =
            rig.root.TransformPoint(new Vector3(-0.10f, -0.45f, -0.20f));

        if (t >= 1f)
        {
            isDashing = false;
            currentState = AnimState.Run;
        }
    }
}
