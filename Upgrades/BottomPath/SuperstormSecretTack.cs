using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.BottomPath;

public class SuperstormSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => BOTTOM;
    public override int Tier => 5;
    public override int Cost => 150100;
    public override int Priority => -2;

    public override string DisplayName => "Superstorm";
    public override string Description => "Shoots 4 superstorms at bloons.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();

        var druid = Game.instance.model.GetTowerFromId("Druid-520");
        var superstormWeapon = druid.GetWeapon(2).Duplicate();

        superstormWeapon.name = "SecretTack-Superstorm";
        superstormWeapon.ejectX = baseWeapon.ejectX;
        superstormWeapon.ejectY = baseWeapon.ejectY;
        superstormWeapon.ejectZ = baseWeapon.ejectZ;
        superstormWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 4, 0f, 360f, null, false, false);

        var projectile = superstormWeapon.projectile;

        if (projectile.HasBehavior<TravelStraitModel>())
        {
            var travel = projectile.GetBehavior<TravelStraitModel>();
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }

        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        superstormWeapon.Rate = 5f;

        attackModel.AddWeapon(superstormWeapon);

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
