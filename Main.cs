using BTD_Mod_Helper;
using MelonLoader;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using SecretTack;

[assembly: MelonInfo(typeof(SecretTack.Main), SecretTack.ModHelperData.Name, SecretTack.ModHelperData.Version, SecretTack.ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace SecretTack;

public class Main : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        base.OnApplicationStart();
        MelonLogger.Msg("[SecretTack] Mod loaded successfully!");
    }

    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        if (tower.towerModel.name.Contains("SecretTack"))
        {
            var tiers = tower.towerModel.tiers;

            if (tiers[0] == 5 && tiers[1] == 5 && tiers[2] == 5)
            {
                if (InGame.instance != null)
                {
                    PopupScreen.instance.ShowPopup(
                        PopupScreen.Placement.menuCenter,
                        "<size=70><b>*** ULTIMATE SECRET TACK ACHIEVED! ***</b></size>",
                        "<size=46>You have unlocked 5-5-5!</size>\n\n" +
                        "<size=58><b><color=yellow>THE ULTIMATE BULLET HELL</color></b></size>\n\n" +
                        "<size=52><b>BONUSES ACTIVATED:</b></size>\n\n" +
                        "<size=40>• <b>3x Attack Speed</b> - Fires three times as fast!\n" +
                        "• <b>50x Damage</b> - Each hit deals 50x damage!\n" +
                        "• <b>50x Pierce</b> - Projectiles hit 50x more bloons!\n" +
                        "• <b>1.2x Projectile Speed</b> - Bullets fly 20% faster\n" +
                        "• <b>Camo Detection</b> - Can see and hit camo bloons\n" +
                        "• <b>Hits All Bloon Types</b> - No immunities!</size>\n\n" +
                        "<size=46><color=yellow><b>Time to absolutely ANNIHILATE!</b></color></size>",
                        new System.Action(() => { }),
                        "OK",
                        null,
                        "",
                        Popup.TransitionAnim.Scale,
                        PopupScreen.BackGround.Grey
                    );
                }
            }
        }
    }
}
