using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using SecretTack;

namespace SecretTack;

public class SecretTack : ModTower
{
    public override TowerSet TowerSet => TowerSet.Primary;
    public override string BaseTower => TowerType.TackShooter;
    public override int Cost => 2100;
    public override string Description => "Spits out projectiles in a 360 angle around the map.";
    public override int TopPathUpgrades => 5;
    public override int MiddlePathUpgrades => 5;
    public override int BottomPathUpgrades => 5;
    public override ParagonMode ParagonMode => ParagonMode.Base555;

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.range = 500f;
        var attackModel = towerModel.GetAttackModel();
        attackModel.range = 500f;

        var weapon = towerModel.GetWeapon();
        weapon.emission = new ArcEmissionModel("ArcEmissionModel_", 32, 0f, 360f, null, false, false);
        weapon.Rate = 1.8f;

        var projectile = weapon.projectile;
        projectile.pierce = 2f;
        projectile.GetDamageModel().damage = 2f;

        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }

        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));
    }
    public override bool IsValidCrosspath(int[] tiers) =>
    ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
}
