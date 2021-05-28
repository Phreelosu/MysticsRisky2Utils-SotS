﻿using BepInEx;
using R2API;
using R2API.Utils;
using R2API.Networking;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using RoR2;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace MysticsRisky2Utils
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(NetworkingAPI), nameof(LanguageAPI), nameof(PrefabAPI), nameof(SoundAPI))]
    public class MysticsRisky2UtilsPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.themysticsword.mysticsrisky2utils";
        public const string PluginName = "MysticsRisky2Utils";
        public const string PluginVersion = "1.0.0";

        internal static BepInEx.Logging.ManualLogSource logger;
        internal const BindingFlags bindingFlagAll = (BindingFlags)(-1);

        public void Awake()
        {
            logger = Logger;

            BaseAssetTypes.BaseAchievement.Init();
            BaseAssetTypes.BaseElite.Init();
            BaseAssetTypes.BaseEquipment.Init();
            BaseAssetTypes.BaseInteractable.Init();
            ChildLocatorAdditions.Init();
            CharacterStats.Init();
            ConCommandHelper.Init();
            CostTypeCreation.Init();
            CustomTempVFXManagement.Init();
            GenericChildLocatorAdditions.Init();
            GenericGameEvents.Init();
            Overlays.Init();
            PlainHologram.Init();
            StateSerializerFix.Init();

            RoR2Application.onLoad += PostGameLoad;
        }

        public void PostGameLoad()
        {
            BaseAssetTypes.BaseItemLike.PostGameLoad();
        }

        public struct GenericCharacterInfo
        {
            public GameObject gameObject;
            public CharacterBody body;
            public CharacterMaster master;
            public TeamComponent teamComponent;
            public HealthComponent healthComponent;
            public Inventory inventory;
            public TeamIndex teamIndex;
            public Vector3 aimOrigin;

            public GenericCharacterInfo(CharacterBody body)
            {
                this.body = body;
                gameObject = body ? body.gameObject : null;
                master = body ? body.master : null;
                teamComponent = body ? body.teamComponent : null;
                healthComponent = body ? body.healthComponent : null;
                inventory = master ? master.inventory : null;
                teamIndex = teamComponent ? teamComponent.teamIndex : TeamIndex.Neutral;
                aimOrigin = body ? body.aimOrigin : Random.insideUnitSphere.normalized;
            }
        }
    }

    public interface IMysticsRisky2UtilsTokenPrefixProvider
    {
        string GetTokenPrefix();
    }
}
