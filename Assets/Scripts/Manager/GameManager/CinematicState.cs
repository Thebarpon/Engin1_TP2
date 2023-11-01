using UnityEngine;

public class CinematicState : IState
{
    protected Cinemachine.CinemachineVirtualCamera m_camera;

    public CinematicState(Cinemachine.CinemachineVirtualCamera camera)
    {
        m_camera = camera;
    }

    public bool CanEnter(IState currentState)
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public bool CanExit()
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public void OnEnter()
    {
        //Play intro cinematic on start
        GameManagerSM._Instance.Intro.Play();
        Debug.Log("On Enter CinematicState");
        m_camera.enabled = true;
    }

    public void OnExit()
    {
        //Stop intro cinematic on exit
        GameManagerSM._Instance.Intro.Stop();
        Debug.Log("On Exit CinematicState");
        m_camera.enabled = false;
    }

    public void OnFixedUpdate()
    {
    }

    public void OnStart()
    {
    }

    public void OnUpdate()
    {
    }
}
