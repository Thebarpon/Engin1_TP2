using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [SerializeField]
    private CharacterControllerStateMachine m_mainCharacter;
    private AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Play sound as long as gamemanager is in gameplaystate and touching the floor, the sound scale with % of max acceleration
        if (m_mainCharacter.IsInContactWithFloor() && GameManagerSM._Instance.GetState() is GameplayState)
        {
            m_audioSource.volume = m_mainCharacter.RB.velocity.magnitude / m_mainCharacter.MaxForwardVelocity;
        }
        else
        {
            m_audioSource.volume = 0;
        }
    }
}
