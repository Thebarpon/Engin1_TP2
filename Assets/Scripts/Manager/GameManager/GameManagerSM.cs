using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManagerSM : BaseStateMachine<IState>
{
    private static GameManagerSM _instance;

    [SerializeField]
    protected Cinemachine.CinemachineVirtualCamera m_gameplayCamera;
    [SerializeField]
    protected Cinemachine.CinemachineVirtualCamera m_cinematicCamera;
    [SerializeField]
    public PlayableDirector Intro;

    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        base.Awake();
    }

    public static GameManagerSM _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManagerSM();
            }

            return _instance;
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadScene();
        }
        base.Update();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("SandBox");
    }
    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new CinematicState(m_cinematicCamera));
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
    }

    //Return current state for sub-state
    public IState GetState()
    {
        return m_currentState;
    }
}