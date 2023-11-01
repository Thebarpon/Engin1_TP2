using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;
    // Start is called before the first frame update
    public void ResetHighFrame()
    {
        Animator.SetBool("IsHit", false);
    }
}
