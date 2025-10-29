using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.TopPath;

public class BladeSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => TOP;
    public override int Tier => 4;
    public override int Cost => 4000;

    public override string DisplayName => "Blade";
    public override string Description => "Shoots 16 blades out. They have a lot of pierce.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var bladeWeapon = Game.instance.model.GetTowerFromId("TackShooter-030").GetWeapon().Duplicate();

        bladeWeapon.name = "SecretTack-Blade";
        bladeWeapon.ejectX = baseWeapon.ejectX;
        bladeWeapon.ejectY = baseWeapon.ejectY;
        bladeWeapon.ejectZ = baseWeapon.ejectZ;
        bladeWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 16, 0f, 360f, null, false, false);
        bladeWeapon.Rate = 3.2f;

        var projectile = bladeWeapon.projectile;
        projectile.pierce += 2f;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        attackModel.AddWeapon(bladeWeapon);
    }
}
