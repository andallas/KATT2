using UnityEngine;

public class SpecialFXHelper : MonoBehaviour
{
    public ParticleSystem fireEffect;
    public ParticleSystem smokeEffect;
    public static SpecialFXHelper Instance { get { return _instance; } }

    private static SpecialFXHelper _instance;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void Explosion(Vector3 position)
    {
        Create(smokeEffect, position);
        Create(fireEffect, position);
    }

    private ParticleSystem Create(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = (ParticleSystem)Instantiate(prefab, position, Quaternion.identity);

        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);

        return newParticleSystem;
    }
}