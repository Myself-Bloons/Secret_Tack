using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace SecretTack.Upgrades.TopPath;

public class BloonSolverSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => TOP;
    public override int Tier => 5;
    public override int Cost => 24000;
    public override int Priority => -2;

    public override string DisplayName => "Bloon Solver";
    public override string Description => "Shoots 12 bloon solving glue blobs out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var glueWeapon = Game.instance.model.GetTowerFromId("GlueGunner-520").GetWeapon().Duplicate();

        glueWeapon.name = "SecretTack-Solver";
        glueWeapon.ejectX = baseWeapon.ejectX;
        glueWeapon.ejectY = baseWeapon.ejectY;
        glueWeapon.ejectZ = baseWeapon.ejectZ;
        glueWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 12, 0f, 360f, null, false, false);
        glueWeapon.Rate = 3.6f;

        var projectile = glueWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        attackModel.AddWeapon(glueWeapon);

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
