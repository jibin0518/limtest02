using UnityEngine;

public enum CharacterAnimState
{
    Idle,
    Walk,
    Attack,
    Hit,
    Death
}

public class AnimeContor : MonoBehaviour
{
    public CharacterAnimState currentState = CharacterAnimState.Idle;

    Transform spine;
    Transform spine1;
    Transform spine2;

    Transform chest;

    Transform rightHandTarget;
    Transform leftHandTarget;
    Transform rightFootTarget;
    Transform leftFootTarget;

    public float idleSpeed = 1.5f;
    public float bodyAngle = 4f;
    public float bodyUpDown = 0.03f;

    public Vector3 rightHandOffset = new Vector3(0.0f, -0.08f, 0.24f);

    public Vector3 leftHandOffset  = new Vector3(-0.05f, -0.05f, 0.18f);

    public Vector3 rightFootOffset = new Vector3(0.25f, -0.08f, 0.02f);

    public Vector3 leftFootOffset  = new Vector3(-0.25f, -0.08f, 0.02f);

    Quaternion spineStart;
    Quaternion spine1Start;
    Quaternion spine2Start;
    Vector3 rootStartPos;

    Transform root;

    void Awake()
    {
        root = transform.Find("Root");
        Transform rig = transform.Find("Rig");

        spine = root.Find("Spine");
        spine1 = root.Find("Spine/Spine1");
        spine2 = root.Find("Spine/Spine1/Spine2");

        chest = spine2;

        rightHandTarget = rig.Find("RightHand_Target");
        leftHandTarget = rig.Find("LeftHand_Target");

        rightFootTarget = rig.Find("RightFoot_Target");
        leftFootTarget = rig.Find("LeftFoot_Target");
    }

    void Start()
    {
        rootStartPos = root.localPosition;

        spineStart = spine.localRotation;
        spine1Start = spine1.localRotation;
        spine2Start = spine2.localRotation;
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case CharacterAnimState.Idle:
                PlayIdle();
                break;
        }
    }

    void PlayIdle()
    {
        float t = Mathf.Sin(Time.time * idleSpeed);

        // 몸 위아래
        root.localPosition = rootStartPos + new Vector3(0, t * bodyUpDown, 0);

        // 몸통 흔들림
        spine.localRotation = spineStart * Quaternion.Euler(t * bodyAngle, 0, 0);
        spine1.localRotation = spine1Start * Quaternion.Euler(t * bodyAngle * 0.5f, 0, 0);
        spine2.localRotation = spine2Start * Quaternion.Euler(t * bodyAngle * 0.3f, 0, 0);

        // 손 가슴 앞으로 모으기
        rightHandTarget.position = chest.TransformPoint(rightHandOffset);
        leftHandTarget.position = chest.TransformPoint(leftHandOffset);

        rightHandTarget.rotation = chest.rotation;
        leftHandTarget.rotation = chest.rotation;

        // 다리 벌리기
        rightFootTarget.position = root.TransformPoint(rightFootOffset);
        leftFootTarget.position = root.TransformPoint(leftFootOffset);
    }
}