using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.MiddlePath;

public class MoltenGrapeSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => MIDDLE;
    public override int Tier => 3;
    public override int Cost => 3600;

    public override string DisplayName => "Molten Grape";
    public override string Description => "Shoots 20 molten grapes out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var moltenGrapeWeapon = Game.instance.model.GetTowerFromId("MonkeyBuccaneer-020").GetWeapon(2).Duplicate();

        moltenGrapeWeapon.name = "SecretTack-MoltenGrape";
        moltenGrapeWeapon.ejectX = baseWeapon.ejectX;
        moltenGrapeWeapon.ejectY = baseWeapon.ejectY;
        moltenGrapeWeapon.ejectZ = baseWeapon.ejectZ;
        moltenGrapeWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 20, 0f, 360f, null, false, false);

        var projectile = moltenGrapeWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        moltenGrapeWeapon.Rate = 2.8f;

        attackModel.AddWeapon(moltenGrapeWeapon);
    }
}
