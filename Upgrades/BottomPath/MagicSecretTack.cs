using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;

namespace SecretTack.Upgrades.BottomPath;

public class MagicSecretTack : ModUpgrade<SecretTack>
{
    public override int Path => BOTTOM;
    public override int Tier => 2;
    public override int Cost => 2150;

    public override string DisplayName => "Magic";
    public override string Description => "Shoots 24 magic bolts out.";

    public override string Portrait => "SecretTack-Portrait";

    public override void ApplyUpgrade(TowerModel tower)
    {
        var attackModel = tower.GetAttackModel();
        var baseWeapon = tower.GetWeapon();
        var magicWeapon = Game.instance.model.GetTowerFromId(TowerType.WizardMonkey).GetWeapon().Duplicate();

        magicWeapon.name = "SecretTack-Magic";
        magicWeapon.ejectX = baseWeapon.ejectX;
        magicWeapon.ejectY = baseWeapon.ejectY;
        magicWeapon.ejectZ = baseWeapon.ejectZ;
        magicWeapon.emission = new ArcEmissionModel("ArcEmissionModel_", 24, 0f, 360f, null, false, false);

        var projectile = magicWeapon.projectile;
        var travel = projectile.GetBehavior<TravelStraitModel>();
        if (travel != null)
        {
            travel.Lifespan = 100f;
            travel.Speed = 53.333332f;
            travel.speedFrames = 8f;
        }
        projectile.AddBehavior(new ExpireProjectileAtScreenEdgeModel("ExpireProjectileAtScreenEdgeModel_"));

        magicWeapon.Rate = 2.4f;

        attackModel.AddWeapon(magicWeapon);
    }
}
