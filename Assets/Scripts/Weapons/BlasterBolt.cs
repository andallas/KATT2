using UnityEngine;

public class BlasterBolt : Projectile
{
    public override void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = isEnemyShot ? SpriteManager.Instance.GetSprite((int)BeamColor.red) : SpriteManager.Instance.GetSprite((int)BeamColor.green);
    }
}