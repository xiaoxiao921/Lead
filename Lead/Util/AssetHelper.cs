using Jotunn.Managers;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Lead.Util
{
    public static class AssetHelper
    {
        public const string AssetBundleName = "item_lead";
        public static AssetBundle LeadAssetBundle;

        public const string LeadPrefabPath = "Assets/Item/Lead.prefab";
        public static GameObject LeadPrefab;

        public const string ChitinHarpoonPrefabName = "projectile_chitinharpoon";
        public const string LeadProjectilePrefabName = "LeadProjectile";
        public const int FangSpearIndex = 1;
        public static GameObject LeadProjectilePrefab;

        public static void Init()
        {
            LeadAssetBundle = GetAssetBundleFromResources(AssetBundleName);
            LeadPrefab = LeadAssetBundle.LoadAsset<GameObject>(LeadPrefabPath);

            ItemManager.OnAfterInit += () => 
            {
                var chitinHarpoon = PrefabManager.Cache.GetPrefab<Projectile>(ChitinHarpoonPrefabName);
                if (chitinHarpoon)
                {
                    LeadProjectilePrefab = PrefabManager.Instance.CreateClonedPrefab(LeadProjectilePrefabName, chitinHarpoon.gameObject);

                    UnityObject.Destroy(LeadProjectilePrefab.transform.GetChild(FangSpearIndex).gameObject);

                    LeadPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_attack.m_attackProjectile = LeadProjectilePrefab;
                }
            };
        }

        public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            var execAssembly = Assembly.GetExecutingAssembly();

            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));

            using var stream = execAssembly.GetManifestResourceStream(resourceName);

            return AssetBundle.LoadFromStream(stream);
        }
    }
}
