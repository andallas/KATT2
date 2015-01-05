using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { ExtraLife, InvulnerabilityBronze, InvulnerabilitySilver, InvulnerabilityGold, ScoreMultiplierX1, ScoreMultiplierX2, ScoreMultiplierX3 }

    public PickupType type;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player player = otherCollider.gameObject.GetComponent<Player>();
        if (player != null)
        {
            switch(type)
            {
                case PickupType.ExtraLife:
                    GameManager.Instance.AddLife();
                break;
                case PickupType.InvulnerabilityBronze:
                case PickupType.InvulnerabilitySilver:
                case PickupType.InvulnerabilityGold:
                    GameManager.Instance.AddInvulnerability(type);
                break;
                case PickupType.ScoreMultiplierX1:
                    GameManager.Instance.AddMultiplier(1);
                break;
                case PickupType.ScoreMultiplierX2:
                    GameManager.Instance.AddMultiplier(2);
                break;
                case PickupType.ScoreMultiplierX3:
                    GameManager.Instance.AddMultiplier(3);
                break;
                default:
                    Debug.LogError("Unimplemented powerup type found.");
                break;
            }
            Destroy(gameObject);
        }
    }
}