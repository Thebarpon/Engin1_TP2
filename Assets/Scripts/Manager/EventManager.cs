using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private CharacterControllerStateMachine MainCharacterStateMachine;
    [SerializeField]
    private EffectManager EffectManager;
    public void EnableRPunchHitbox()
    {
        MainCharacterStateMachine.EnableHitbox(MainCharacterStateMachine.RPunchHitbox);
    }
    public void DisableRPunchHitbox()
    {
        MainCharacterStateMachine.DisableHitbox(MainCharacterStateMachine.RPunchHitbox);
    }
    public void OnJumpLanding(Vector3 landingPosition)
    {
        EffectManager.InstantiateEffect(EffectManager.EEffectType.StartJump, landingPosition);
    }
}
