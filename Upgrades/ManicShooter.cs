using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using MelonLoader;

namespace SecretTack.Upgrades;

public class ManicShooter : ModParagonUpgrade<SecretTack>
{
    public override int Cost => 950000;
    public override string DisplayName => "Manic Shooter";
    public override string Description => "LAG ALERT. Ultimate Bullet Hell";
    public override string Icon => "ManicShooter-Icon";
    public override string Portrait => "ManicShooter-Portrait";


    public override void ApplyUpgrade(TowerModel towerModel)
    {
        foreach (var weaponModel in towerModel.GetWeapons())
        {
            weaponModel.rate *= 0.5f;
            weaponModel.projectile.pierce *= 10f;

            var damageModel = weaponModel.projectile.GetDamageModel();
            if (damageModel != null)
            {
                damageModel.damage *= 10f;
                damageModel.immuneBloonProperties = Il2Cpp.BloonProperties.None;
            }
        }

        towerModel.range += 100f;
        towerModel.GetAttackModel().range += 100f;
        towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
    }
}
