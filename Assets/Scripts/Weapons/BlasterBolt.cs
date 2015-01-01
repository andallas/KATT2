using UnityEngine;

public class BlasterBolt : Projectile
{
    private override void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = isEnemyShot ? SpriteManager.Instance.GetSprite("redBlasterBolt") : SpriteManager.Instance.GetSprite("greenBlasterBolt");
    }
}