using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.MiddlePath;

public class GrapeSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => MIDDLE;
    public override int Tier => 2;
    public override int Cost => 2900;

    public override string DisplayName => "Grape";
    public override string Description => "Shoots 24 grapes out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var grapeWeapon = Game.instance.model.GetTowerFromId("MonkeyBuccaneer-010").GetWeapon(2).Duplicate();

        grapeWeapon.name = "SecretTack-Grape";
        grapeWeapon.ejectX = baseWeapon.ejectX;
        grapeWeapon.ejectY = baseWeapon.ejectY;
        grapeWeapon.ejectZ = baseWeapon.ejectZ;
        grapeWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 24, 0f, 360f, null, false, false);

        var projectile = grapeWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        grapeWeapon.Rate = 2.4f;

        attackModel.AddWeapon(grapeWeapon);
    }
}
