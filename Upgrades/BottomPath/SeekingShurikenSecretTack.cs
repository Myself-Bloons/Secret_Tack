using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.BottomPath;

public class SeekingShurikenSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => BOTTOM;
    public override int Tier => 3;
    public override int Cost => 2250;

    public override string DisplayName => "Seeking Shuriken";
    public override string Description => "Shoots 20 shurikens that seek out bloons.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var seekingShurikenWeapon = Game.instance.model.GetTowerFromId("NinjaMonkey-003").GetWeapon().Duplicate();

        seekingShurikenWeapon.name = "SecretTack-SeekingShuriken";
        seekingShurikenWeapon.ejectX = baseWeapon.ejectX;
        seekingShurikenWeapon.ejectY = baseWeapon.ejectY;
        seekingShurikenWeapon.ejectZ = baseWeapon.ejectZ;
        seekingShurikenWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 20, 0f, 360f, null, false, false);

        var projectile = seekingShurikenWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        seekingShurikenWeapon.Rate = 2.8f;

        attackModel.AddWeapon(seekingShurikenWeapon);
    }
}
