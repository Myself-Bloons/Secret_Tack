using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.MiddlePath;

public class DartlingSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override int Cost => 1600;

    public override string DisplayName => "Dartling";
    public override string Description => "Shoots 28 dartling gunner darts out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var dartlingWeapon = Game.instance.model.GetTowerFromId(TowerType.DartlingGunner).GetWeapon().Duplicate();

        dartlingWeapon.name = "SecretTack-Dartling";
        dartlingWeapon.ejectX = baseWeapon.ejectX;
        dartlingWeapon.ejectY = baseWeapon.ejectY;
        dartlingWeapon.ejectZ = baseWeapon.ejectZ;
        dartlingWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 28, 0f, 360f, null, false, false);

        var projectile = dartlingWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        dartlingWeapon.Rate = 2f;

        attackModel.AddWeapon(dartlingWeapon);
    }
}
