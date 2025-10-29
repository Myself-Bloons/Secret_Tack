using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.TopPath;

public class MOABMaulerSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => TOP;
    public override int Tier => 3;
    public override int Cost => 4800;

    public override string DisplayName => "MOAB Mauler";
    public override string Description => "Shoots 20 MOAB-mauling bombs out. They deal extra damage to MOABS.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var bombWeapon = Game.instance.model.GetTowerFromId("BombShooter-230").GetWeapon().Duplicate();

        bombWeapon.name = "SecretTack-Mauler";
        bombWeapon.ejectX = baseWeapon.ejectX;
        bombWeapon.ejectY = baseWeapon.ejectY;
        bombWeapon.ejectZ = baseWeapon.ejectZ;
        bombWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 20, 0f, 360f, null, false, false);
        bombWeapon.Rate = 2.8f;

        var projectile = bombWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        attackModel.AddWeapon(bombWeapon);
    }
}
