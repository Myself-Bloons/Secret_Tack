using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.MiddlePath;

public class MADSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override int Cost => 70500;
    public override int Priority => -2;

    public override string DisplayName => "MAD";
    public override string Description => "Shoots 12 MAD projectiles out. Each MAD projectile deals more damage to MOABS.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var madWeapon = Game.instance.model.GetTowerFromId("DartlingGunner-052").GetWeapon().Duplicate();

        madWeapon.name = "SecretTack-MAD";
        madWeapon.ejectX = baseWeapon.ejectX;
        madWeapon.ejectY = baseWeapon.ejectY;
        madWeapon.ejectZ = baseWeapon.ejectZ;
        madWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 12, 0f, 360f, null, false, false);

        var projectile = madWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        madWeapon.Rate = 3.6f;

        attackModel.AddWeapon(madWeapon);

        if (tower.tiers[0] == 5 && tower.tiers[1] == 5 && tower.tiers[2] == 5)
        {
            ApplyUltimateBonuses(tower);
        }
    }

    private void ApplyUltimateBonuses(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();

        tower.GetDescendants<Il2CppAssets.Scripts.Models.Towers.Filters.FilterInvisibleModel>().ForEach(model => model.isActive = false);
        tower.towerSelectionMenuThemeId = "Camo";

        foreach (var weaponModel in attackModel.weapons)
        {
            weaponModel.Rate /= 3f;

            var projectile = weaponModel.projectile;

            projectile.pierce *= 50f;

            if (projectile.HasBehavior<TravelStraitModel>())
            {
                var travel = projectile.GetBehavior<TravelStraitModel>();
                travel.Speed *= 1.2f;
                travel.speedFrames *= 1.2f;
            }

            var damageModel = projectile.GetDamageModel();
            if (damageModel != null)
            {
                damageModel.damage *= 50f;
                damageModel.immuneBloonProperties = 0;
            }
        }

        tower.ApplyDisplay<SecretTack555.SecretTack555Display>();
    }
}
