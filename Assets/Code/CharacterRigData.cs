using UnityEngine;

public class CharacterRigData
{
    public Transform root;
    public Transform spine;
    public Transform spine1;
    public Transform spine2;
    public Transform chest;

    public Transform rightHandTarget;
    public Transform leftHandTarget;
    public Transform rightFootTarget;
    public Transform leftFootTarget;
    public Transform rightFootHint;
    public Transform leftFootHint;

    public CharacterRigData(Transform character)
    {
        root = character.Find("Root");

        spine = root.Find("Spine");
        spine1 = root.Find("Spine/Spine1");
        spine2 = root.Find("Spine/Spine1/Spine2");
        chest = spine2;

        Transform rig = character.Find("Rig");

        rightHandTarget = rig.Find("RightHand_Target");
        leftHandTarget = rig.Find("LeftHand_Target");
        rightFootTarget = rig.Find("RightFoot_Target");
        leftFootTarget = rig.Find("LeftFoot_Target");
        rightFootHint = rig.Find("RightFoot_Hint");
        leftFootHint = rig.Find("LeftFoot_Hint");
    }
}