using System.Linq;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

namespace SecretTack.Displays;

public class SecretTackBaseDisplay : ModTowerDisplay<SecretTack>
{
    public override string BaseDisplay => GetDisplay(TowerType.TackShooter);

    public override bool UseForTower(int[] tiers)
    {
        return !(tiers[0] == 5 && tiers[1] == 5 && tiers[2] == 5);
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        SetMeshTexture(node, "SecretTackBaseDiplay");
    }
}
