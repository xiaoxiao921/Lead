using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Lead.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Lead.Items.Lead
{
    public static class ItemData
    {
        public static CustomItem CustomItem;
        public static CustomRecipe CustomRecipe;

        public const string TokenName = "$custom_item_lead";
        public const string TokenValue = "Lead";

        public const string TokenDescriptionName = "$custom_item_lead_description";
        public const string TokenDescriptionValue = "Leads are used to leash and lead passive mobs";

        public const string CraftingStationPrefabName = "piece_workbench";

        public const string TokenLanguage = "English";

        internal static void Init()
        {
            AddCustomRecipe();
            AddCustomItem();

            LocalizationManager.Instance.AddLocalization(new LocalizationConfig(TokenLanguage)
            {
                Translations = 
                {
                    { TokenName.Trim('$'), TokenValue },
                    { TokenDescriptionName.Trim('$'), TokenDescriptionValue }
                }
            });
        }

        private static void AddCustomRecipe()
        {
            var recipe = ScriptableObject.CreateInstance<Recipe>();

            recipe.m_item = AssetHelper.LeadPrefab.GetComponent<ItemDrop>();

            var neededResources = new List<Piece.Requirement>
            {
                MockRequirement.Create("Ooze", 4),
                MockRequirement.Create("DeerHide", 2),
            };

            recipe.m_resources = neededResources.ToArray();

            CustomRecipe = new CustomRecipe(recipe, false, true);
            ItemManager.Instance.AddRecipe(CustomRecipe);
        }

        private static void AddCustomItem()
        {
            CustomItem = new CustomItem(AssetHelper.LeadPrefab, true);
            ItemManager.Instance.AddItem(CustomItem);
        }
    }
}
