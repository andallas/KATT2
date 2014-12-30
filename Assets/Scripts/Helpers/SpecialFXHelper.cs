using UnityEngine;

public class SpecialFXHelper : MonoBehaviour
{
    public static SpecialFXHelper Instance;
    public ParticleSystem fireEffect;
    public ParticleSystem smokeEffect;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("ERROR: Multiple instances of SpecialFXHelper.");
        }

        Instance = this;
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