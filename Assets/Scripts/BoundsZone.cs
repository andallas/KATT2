using UnityEngine;

public class BoundsZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Beam beam = other.gameObject.GetComponent<Beam>();
        if(beam != null)
        {
            Destroy(beam.gameObject);
        }
    }
}