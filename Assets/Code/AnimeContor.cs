using UnityEngine;

public enum AnimationState
{
    Idle,
    Walk,
    Run,
    Attack,
    Hit,
    Death
}

public class AnimeContor : MonoBehaviour
{
    public AnimationState currentState = AnimationState.Idle;

    public IdleBodyMotion body;
    public IdleHandPose hands;
    public IdleLegPose legs;

    void Update()
    {
        switch (currentState)
        {
            case AnimationState.Idle:
                body.enabled = true;
                hands.enabled = true;
                legs.enabled = true;
                break;

            default:
                body.enabled = false;
                hands.enabled = false;
                legs.enabled = false;
                break;
        }
    }
}

