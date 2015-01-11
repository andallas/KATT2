using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType {InvulnerabilityBronze, InvulnerabilitySilver, InvulnerabilityGold,
                            MedalBronze, MedalSilver, MedalGold,
                            ScoreMultiplierX1, ScoreMultiplierX2, ScoreMultiplierX3,
                            CoinBronze, CoinSilver, CoinGold,
                            ExtraLife }

    public PickupType type;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player player = otherCollider.gameObject.GetComponent<Player>();
        if (player != null)
        {
            switch(type)
            {
                case PickupType.InvulnerabilityBronze:
                case PickupType.InvulnerabilitySilver:
                case PickupType.InvulnerabilityGold:
                    GameManager.Instance.AddInvulnerability(((int)type % 3 + 1) * 5f);
                    break;
                case PickupType.MedalBronze:
                case PickupType.MedalSilver:
                case PickupType.MedalGold:
                    GameManager.Instance.AddMedal(((int)type % 3 + 1) * 500);
                    break;
                case PickupType.ScoreMultiplierX1:
                case PickupType.ScoreMultiplierX2:
                case PickupType.ScoreMultiplierX3:
                    GameManager.Instance.AddMultiplier((int)type % 3 + 1);
                    break;
                case PickupType.CoinBronze:
                case PickupType.CoinSilver:
                case PickupType.CoinGold:
                    GameManager.Instance.AddCores((int)type % 3 + 1);
                    break;
                case PickupType.ExtraLife:
                    GameManager.Instance.AddLife();
                    break;
                default:
                    Debug.LogError("Pickup type not implemented.");
                    break;
            }
            Destroy(gameObject);
        }
    }

    public void OutOfBounds()
    {
        switch (type)
        {
            case PickupType.MedalBronze:
            case PickupType.MedalSilver:
            case PickupType.MedalGold:
                GameManager.Instance.ResetMedalMultiplier();
                Destroy(gameObject);
            break;
            case PickupType.InvulnerabilityBronze:
            case PickupType.InvulnerabilitySilver:
            case PickupType.InvulnerabilityGold:
            case PickupType.ScoreMultiplierX1:
            case PickupType.ScoreMultiplierX2:
            case PickupType.ScoreMultiplierX3:
            case PickupType.CoinBronze:
            case PickupType.CoinSilver:
            case PickupType.CoinGold:
            case PickupType.ExtraLife:
                Destroy(gameObject);
            break;
            default:
                Debug.LogError("Pickup type not implemented.");
            break;
        }
    }
}