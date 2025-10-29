using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.BottomPath;

public class ShurikenSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => BOTTOM;
    public override int Tier => 1;
    public override int Cost => 1900;

    public override string DisplayName => "Shuriken";
    public override string Description => "Shoots 28 shurikens out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var shurikenWeapon = Game.instance.model.GetTowerFromId("NinjaMonkey-100").GetWeapon().Duplicate();

        shurikenWeapon.name = "SecretTack-Shuriken";
        shurikenWeapon.ejectX = baseWeapon.ejectX;
        shurikenWeapon.ejectY = baseWeapon.ejectY;
        shurikenWeapon.ejectZ = baseWeapon.ejectZ;
        shurikenWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 28, 0f, 360f, null, false, false);

        var projectile = shurikenWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        shurikenWeapon.Rate = 2f;

        attackModel.AddWeapon(shurikenWeapon);
    }
}
