using Cinemachine;
using UnityEngine;

public class HitState : CharacterState
{
    private const float HIT_DURATION = 0.4f;
    private float m_currentStateDuration;
    private const float MAX_SHAKE_FORCE = 5.0f;
    private float m_currentShakeForce;

    public override void OnEnter()
    {
        m_currentStateDuration = HIT_DURATION;
        m_stateMachine.OnHitStimuliReceived = false;

        //Set the animation bool that allow highframe
        m_stateMachine.Animator.SetTrigger("OnHit");

        //Set shake to max
        SetAmpFreqNoise(MAX_SHAKE_FORCE);
        m_currentShakeForce = MAX_SHAKE_FORCE;
        Debug.Log("Enter state: HitState\n");
    }

    public override void OnExit()
    {
        //Set shake to 0 on exit
        SetAmpFreqNoise(0);
        Debug.Log("Exit state: HitState\n");
    }

    public override void OnFixedUpdate()
    {
        m_stateMachine.FixedUpdateQuickDeceleration();

        //Reduce shake every frame
        m_currentShakeForce -= Time.fixedDeltaTime * (MAX_SHAKE_FORCE / HIT_DURATION);
        SetAmpFreqNoise(m_currentShakeForce);
    }

    private void SetAmpFreqNoise(float shakeForce)
    {
        m_stateMachine.Cine_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeForce;
        m_stateMachine.Cine_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = shakeForce;
    }

    public override void OnUpdate()
    {
        m_currentStateDuration -= Time.deltaTime;
    }

    public override bool CanEnter(IState currentState)
    {
        return m_stateMachine.OnHitStimuliReceived;
    }

    public override bool CanExit()
    {
        return m_currentStateDuration < 0;
    }
}
