using System.Collections.Generic;
using UnityEngine;

public class POHitbox : MonoBehaviour
{
    [SerializeField]
    protected bool m_canHit;
    [SerializeField]
    protected bool m_canReceiveHit;
    [SerializeField]
    protected EUnitType m_unitType = EUnitType.Count;
    [SerializeField]
    protected List<EUnitType> m_affectedUnitType = new List<EUnitType>();
    [SerializeField]
    protected GameObject AttachedObject;
    [SerializeField]
    protected Animator ParentAnimator;

    protected void OnTriggerEnter(Collider other)
    {
        var otherHitbox = other.GetComponent<POHitbox>();

        if (otherHitbox != null)
        {
            if (CanHitOther(otherHitbox) && !otherHitbox.ParentAnimator.GetBool("IsHit"))
            {
                EffectManager._Instance.InstantiateEffect(EffectManager.EEffectType.Attack, other.ClosestPoint(transform.position));
                this.enabled = false;
                otherHitbox.GetHit(this);
            }
        }
        else
        {
            return;
        }
    }

    protected bool CanHitOther(POHitbox otherHitbox)
    {
        return m_canHit &&
            otherHitbox.m_canReceiveHit &&
            m_affectedUnitType.Contains(otherHitbox.m_unitType);
    }

    protected void GetHit(POHitbox otherHitbox)
    {
        Debug.Log(gameObject.name + "Got hit by" + otherHitbox);
        ParentAnimator.SetBool("IsHit", true);
        ParentAnimator.SetTrigger("OnHit");
    }
    public enum EUnitType
    {
        Ally,
        Enemy,
        Neutral,
        Count
    }
}
