using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.TopPath;

public class DartSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => TOP;
    public override int Tier => 1;
    public override int Cost => 1600;

    public override string DisplayName => "Dart";
    public override string Description => "Shoots 28 darts out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var dartWeapon = Game.instance.model.GetTowerFromId(TowerType.DartMonkey).GetWeapon().Duplicate();

        dartWeapon.name = "SecretTack-Dart";
        dartWeapon.ejectX = baseWeapon.ejectX;
        dartWeapon.ejectY = baseWeapon.ejectY;
        dartWeapon.ejectZ = baseWeapon.ejectZ;
        dartWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 28, 0f, 360f, null, false, false);
        dartWeapon.Rate = 2f;

        var projectile = dartWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        attackModel.AddWeapon(dartWeapon);
    }
}
