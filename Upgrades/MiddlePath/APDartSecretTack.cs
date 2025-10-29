using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.MiddlePath;

public class APDartSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => MIDDLE;
    public override int Tier => 4;
    public override int Cost => 7900;

    public override string DisplayName => "AP Dart";
    public override string Description => "Shoots 16 AP darts out. Each dart deals more damage to MOABS.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var apDartWeapon = Game.instance.model.GetTowerFromId("MonkeySub-024").GetWeapon().Duplicate();

        apDartWeapon.name = "SecretTack-APDart";
        apDartWeapon.ejectX = baseWeapon.ejectX;
        apDartWeapon.ejectY = baseWeapon.ejectY;
        apDartWeapon.ejectZ = baseWeapon.ejectZ;
        apDartWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 16, 0f, 360f, null, false, false);

        var projectile = apDartWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        apDartWeapon.Rate = 3.2f;

        attackModel.AddWeapon(apDartWeapon);
    }
}
