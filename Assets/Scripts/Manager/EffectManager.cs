using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;

    //Custom dictionary inspector interface
    [SerializeField]
    public List<SParticleffect> ParticleSytemEntries = new List<SParticleffect>();
    public Dictionary<EEffectType, SParticleffect> PSEffect = new Dictionary<EEffectType, SParticleffect>();

    public void Awake()
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

        //Allocate dictionary entry
        foreach (var entry in ParticleSytemEntries)
        {
            if (!PSEffect.ContainsKey(entry.m_effectType))
            {
                PSEffect.Add(entry.m_effectType, entry);
            }
        }
    }

    public static EffectManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EffectManager();
            }

            return _instance;
        }
    }

    public void InstantiateEffect(EEffectType effectType, Vector3 effectPosition)
    {
        //manage the instantiation type
        switch (effectType)
        {
            case EEffectType.Attack:
                OnAttack(effectPosition);
                break;
            case EEffectType.StartJump:
                OnJump(effectPosition);
                break;
            case EEffectType.Count: break;
        }
    }

    public enum EEffectType
    {
        Attack,
        StartJump,
        Count
    }

    private void OnAttack(Vector3 effectPosition)
    {
        //Instantiate components at the position of the effect
        ParticleSystem newParticleSystem = Instantiate(PSEffect[EEffectType.Attack].m_particleSystem, effectPosition, Quaternion.identity);
        AudioSource.PlayClipAtPoint(PSEffect[EEffectType.Attack].m_audioClip, effectPosition);
    }

    private void OnJump(Vector3 effectPosition)
    {
        //Instantiate components at the position of the effect
        AudioSource.PlayClipAtPoint(PSEffect[EEffectType.StartJump].m_audioClip, effectPosition, PSEffect[EEffectType.StartJump].m_volume);
    }

    [System.Serializable]
    public struct SParticleffect
    {
        public EEffectType m_effectType;
        public ParticleSystem m_particleSystem;
        public AudioClip m_audioClip;
        [Range(0.0f, 1.0f)]
        public float m_volume;
    }
}
