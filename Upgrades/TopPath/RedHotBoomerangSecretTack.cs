using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.TopPath;

public class RedHotBoomerangSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => TOP;
    public override int Tier => 2;
    public override int Cost => 3200;

    public override string DisplayName => "Red Hot Boomerang";
    public override string Description => "Shoots 24 red hot boomerangs out. They can pop leads.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var boomerangWeapon = Game.instance.model.GetTowerFromId("BoomerangMonkey-002").GetWeapon().Duplicate();

        boomerangWeapon.name = "SecretTack-Boomerang";
        boomerangWeapon.ejectX = baseWeapon.ejectX;
        boomerangWeapon.ejectY = baseWeapon.ejectY;
        boomerangWeapon.ejectZ = baseWeapon.ejectZ;
        boomerangWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 24, 0f, 360f, null, false, false);
        boomerangWeapon.Rate = 2.4f;

        var projectile = boomerangWeapon.projectile;
        projectile.RemoveBehavior<FollowPathModel>();
        var travelBehavior = baseWeapon.projectile.GetBehavior<TravelStraitModel>();
        if (travelBehavior != null)
        {
            projectile.AddBehavior(travelBehavior.Duplicate());
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        attackModel.AddWeapon(boomerangWeapon);
    }
}
