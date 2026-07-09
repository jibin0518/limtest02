using UnityEngine;

public enum CharacterAnimState
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
    public CharacterAnimState currentState = CharacterAnimState.Idle;

    CharacterRigData rig;
    IdleMotion idle;
    RunMotion runMotion;

    void Awake()
    {
        rig = new CharacterRigData(transform);
        idle = new IdleMotion(rig);
        runMotion = new RunMotion(rig);
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case CharacterAnimState.Idle:
                idle.Play();
                break;
            case CharacterAnimState.Run:
                runMotion.Play();
                break;
        }
    }
}