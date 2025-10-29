using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.BottomPath;

public class SunBeamSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => BOTTOM;
    public override int Tier => 4;
    public override int Cost => 11500;

    public override string DisplayName => "Sun Beam";
    public override string Description => "Shoots 16 sun beams at bloons.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var sunBeamWeapon = Game.instance.model.GetTowerFromId("SuperMonkey-300").GetWeapon().Duplicate();

        sunBeamWeapon.name = "SecretTack-SunBeam";
        sunBeamWeapon.ejectX = baseWeapon.ejectX;
        sunBeamWeapon.ejectY = baseWeapon.ejectY;
        sunBeamWeapon.ejectZ = baseWeapon.ejectZ;
        sunBeamWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 16, 0f, 360f, null, false, false);

        var projectile = sunBeamWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        projectile.pierce += 7f;
        sunBeamWeapon.Rate = 3.2f;

        attackModel.AddWeapon(sunBeamWeapon);
    }
}
