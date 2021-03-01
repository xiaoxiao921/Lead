using System;
using System.Text;
using UnityEngine;

namespace Lead.Util
{
    internal static class Debug
    {
        internal static void Init()
        {
            On.Humanoid.IsTeleportable += CanAlwaysTeleport;
        }

        private static bool CanAlwaysTeleport(On.Humanoid.orig_IsTeleportable orig, Humanoid self)
        {
            return true;
        }

        internal static void Dump(this ObjectDB objectDB)
        {
            var consumablesTable = new StringBuilder();
            consumablesTable.Append("{| class=\"wikitable\"" + Environment.NewLine +
                                 "|+ Consumables" + Environment.NewLine +
                                 "! Index" + Environment.NewLine +
                                 "! Prefab Name" + Environment.NewLine +
                                 "! Token Name" + Environment.NewLine +
                                 "! Localized Name" + Environment.NewLine +
                                 "! Max Stack Size" + Environment.NewLine +
                                 "! Food" + Environment.NewLine +
                                 "! Food Stamina" + Environment.NewLine +
                                 "! Food Duration" + Environment.NewLine +
                                 "! Food Regen" + Environment.NewLine +
                                 "|-" + Environment.NewLine + Environment.NewLine);

            void ConsumableItemInfo(GameObject itemGameObject, ItemDrop.ItemData item, ref int ind)
            {
                consumablesTable.Append($"| {ind++}{Environment.NewLine}");
                consumablesTable.Append($"| {itemGameObject.name}{Environment.NewLine}");
                consumablesTable.Append($"| {item.m_shared.m_name}{Environment.NewLine}");
                consumablesTable.Append($"| {Localization.instance.Localize(item.m_shared.m_name)}{Environment.NewLine}");
                consumablesTable.Append($"| {item.m_shared.m_maxStackSize}{Environment.NewLine}");

                if (item.m_shared.m_food > 0f)
                {
                    consumablesTable.Append($"| {item.m_shared.m_food}{Environment.NewLine}");
                    consumablesTable.Append($"| {item.m_shared.m_foodStamina}{Environment.NewLine}");
                    consumablesTable.Append($"| {item.m_shared.m_foodBurnTime}{Environment.NewLine}");
                    consumablesTable.Append($"| {item.m_shared.m_foodRegen}{Environment.NewLine}");
                }

                consumablesTable.Append($"|-{Environment.NewLine}");
            }

            var weaponsTable = new StringBuilder();
            weaponsTable.Append("{| class=\"wikitable\"" + Environment.NewLine +
                                 "|+ Weapons" + Environment.NewLine +
                                 "! Index" + Environment.NewLine +
                                 "! Prefab Name" + Environment.NewLine +
                                 "! Token Name" + Environment.NewLine +
                                 "! Localized Name" + Environment.NewLine +
                                 "! Max Stack Size" + Environment.NewLine +
                                 "! Max Quality" + Environment.NewLine +
                                 "! Max Durability" + Environment.NewLine +
                                 "! Damage (Phys + Ele = Total)" + Environment.NewLine +
                                 "! Per Quality" + Environment.NewLine +
                                 "! Knockback" + Environment.NewLine +
                                 "! Backstab Bonus" + Environment.NewLine +
                                 "! Block Power" + Environment.NewLine +
                                 "! Deflection Force" + Environment.NewLine +
                                 "! Per Quality" + Environment.NewLine +
                                 "! Timed Parry Bonus" + Environment.NewLine +
                                 "! Ammo Type" + Environment.NewLine +
                                 "|-" + Environment.NewLine + Environment.NewLine);

            void WeaponItemInfo(GameObject itemGameObject, ItemDrop.ItemData item, ref int ind)
            {
                weaponsTable.Append($"| {ind++}{Environment.NewLine}");
                weaponsTable.Append($"| {itemGameObject.name}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_name}{Environment.NewLine}");
                weaponsTable.Append($"| {Localization.instance.Localize(item.m_shared.m_name)}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_maxStackSize}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_maxQuality}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_maxDurability}{Environment.NewLine}");

                weaponsTable.Append($"| {item.m_shared.m_damages.GetTotalPhysicalDamage()} + {item.m_shared.m_damages.GetTotalElementalDamage()} = {item.m_shared.m_damages.GetTotalDamage()}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_damagesPerLevel.GetTotalPhysicalDamage()} + {item.m_shared.m_damagesPerLevel.GetTotalElementalDamage()} = {item.m_shared.m_damagesPerLevel.GetTotalDamage()}{Environment.NewLine}");

                weaponsTable.Append($"| {item.m_shared.m_attackForce}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_backstabBonus}{Environment.NewLine}");

                weaponsTable.Append($"| {item.m_shared.m_blockPower}{Environment.NewLine}");

                weaponsTable.Append($"| {item.m_shared.m_deflectionForce}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_deflectionForcePerLevel}{Environment.NewLine}");
                weaponsTable.Append($"| {item.m_shared.m_timedBlockBonus}{Environment.NewLine}");

                weaponsTable.Append($"| {item.m_shared.m_ammoType}{Environment.NewLine}");

                weaponsTable.Append($"|-{Environment.NewLine}");
            }

            var shieldsTable = new StringBuilder();
            shieldsTable.Append("{| class=\"wikitable\"" + Environment.NewLine +
                                 "|+ Shields" + Environment.NewLine +
                                 "! Index" + Environment.NewLine +
                                 "! Prefab Name" + Environment.NewLine +
                                 "! Token Name" + Environment.NewLine +
                                 "! Localized Name" + Environment.NewLine +
                                 "! Teleportable" + Environment.NewLine +
                                 "! Max Quality" + Environment.NewLine +
                                 "! Max Durability" + Environment.NewLine +
                                 "! Block Power" + Environment.NewLine +
                                 "! Per Quality" + Environment.NewLine +
                                 "! Deflection Force" + Environment.NewLine +
                                 "! Per Quality" + Environment.NewLine +
                                 "! Timed Parry Bonus" + Environment.NewLine +
                                 "|-" + Environment.NewLine + Environment.NewLine);

            void ShieldItemInfo(GameObject itemGameObject, ItemDrop.ItemData item, ref int ind)
            {
                shieldsTable.Append($"| {ind++}{Environment.NewLine}");
                shieldsTable.Append($"| {itemGameObject.name}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_name}{Environment.NewLine}");
                shieldsTable.Append($"| {Localization.instance.Localize(item.m_shared.m_name)}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_teleportable}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_maxQuality}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_maxDurability}{Environment.NewLine}");

                shieldsTable.Append($"| {item.m_shared.m_blockPower}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_blockPowerPerLevel}{Environment.NewLine}");

                shieldsTable.Append($"| {item.m_shared.m_deflectionForce}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_deflectionForcePerLevel}{Environment.NewLine}");
                shieldsTable.Append($"| {item.m_shared.m_timedBlockBonus}{Environment.NewLine}");

                shieldsTable.Append($"|-{Environment.NewLine}");
            }

            var armorsTable = new StringBuilder();
            armorsTable.Append("{| class=\"wikitable\"" + Environment.NewLine +
                                 "|+ Armors" + Environment.NewLine +
                                 "! Index" + Environment.NewLine +
                                 "! Prefab Name" + Environment.NewLine +
                                 "! Token Name" + Environment.NewLine +
                                 "! Localized Name" + Environment.NewLine +
                                 "! Max Quality" + Environment.NewLine +
                                 "! Max Durability" + Environment.NewLine +
                                 "! Armor Value" + Environment.NewLine +
                                 "! Per Quality" + Environment.NewLine +
                                 "! Movement Modifier" + Environment.NewLine +
                                 "|-" + Environment.NewLine + Environment.NewLine);

            void ArmorItemInfo(GameObject itemGameObject, ItemDrop.ItemData item, ref int ind)
            {
                armorsTable.Append($"| {ind++}{Environment.NewLine}");
                armorsTable.Append($"| {itemGameObject.name}{Environment.NewLine}");
                armorsTable.Append($"| {item.m_shared.m_name}{Environment.NewLine}");
                armorsTable.Append($"| {Localization.instance.Localize(item.m_shared.m_name)}{Environment.NewLine}");
                armorsTable.Append($"| {item.m_shared.m_maxQuality}{Environment.NewLine}");
                armorsTable.Append($"| {item.m_shared.m_maxDurability}{Environment.NewLine}");

                armorsTable.Append($"| {item.m_shared.m_armor}{Environment.NewLine}");
                armorsTable.Append($"| {item.m_shared.m_armorPerLevel}{Environment.NewLine}");

                if (item.m_shared.m_movementModifier != 0f)
                {
                    armorsTable.Append($"| {item.m_shared.m_movementModifier * 100f:+0;-0}{Environment.NewLine}");
                }

                armorsTable.Append($"|-{Environment.NewLine}");
            }

            var ammoTable = new StringBuilder();
            ammoTable.Append("{| class=\"wikitable\"" + Environment.NewLine +
                                 "|+ Ammos" + Environment.NewLine +
                                 "! Index" + Environment.NewLine +
                                 "! Prefab Name" + Environment.NewLine +
                                 "! Token Name" + Environment.NewLine +
                                 "! Localized Name" + Environment.NewLine +
                                 "! Max Stack Size" + Environment.NewLine +
                                 "! Damage (Phys + Ele = Total)" + Environment.NewLine +
                                 "! Knockback" + Environment.NewLine +
                                 "|-" + Environment.NewLine + Environment.NewLine);

            void AmmoItemInfo(GameObject itemGameObject, ItemDrop.ItemData item, ref int ind)
            {
                ammoTable.Append($"| {ind++}{Environment.NewLine}");
                ammoTable.Append($"| {itemGameObject.name}{Environment.NewLine}");
                ammoTable.Append($"| {item.m_shared.m_name}{Environment.NewLine}");
                ammoTable.Append($"| {Localization.instance.Localize(item.m_shared.m_name)}{Environment.NewLine}");
                ammoTable.Append($"| {item.m_shared.m_maxStackSize}{Environment.NewLine}");

                ammoTable.Append($"| {item.m_shared.m_damages.GetTotalPhysicalDamage()} + {item.m_shared.m_damages.GetTotalElementalDamage()} = {item.m_shared.m_damages.GetTotalDamage()}{Environment.NewLine}");
                ammoTable.Append($"| {item.m_shared.m_attackForce}{Environment.NewLine}");

                ammoTable.Append($"|-{Environment.NewLine}");
            }

            var otherItemsTable = new StringBuilder();
            otherItemsTable.Append("{| class=\"wikitable\"" + Environment.NewLine +
                                 "|+ Items" + Environment.NewLine +
                                 "! Index" + Environment.NewLine +
                                 "! Prefab Name" + Environment.NewLine +
                                 "! Token Name" + Environment.NewLine +
                                 "! Localized Name" + Environment.NewLine +
                                 "! Type" + Environment.NewLine +
                                 "! Teleportable" + Environment.NewLine +
                                 "! Value" + Environment.NewLine +
                                 "! Max Stack Size" + Environment.NewLine +
                                 "! Max Quality" + Environment.NewLine +
                                 "|-" + Environment.NewLine + Environment.NewLine);

            void OtherItemInfo(GameObject itemGameObject, ItemDrop.ItemData item, ref int ind)
            {
                otherItemsTable.Append($"| {ind++}{Environment.NewLine}");
                otherItemsTable.Append($"| {itemGameObject.name}{Environment.NewLine}");
                otherItemsTable.Append($"| {item.m_shared.m_name}{Environment.NewLine}");
                otherItemsTable.Append($"| {Localization.instance.Localize(item.m_shared.m_name)}{Environment.NewLine}");
                otherItemsTable.Append($"| {item.m_shared.m_itemType}{Environment.NewLine}");
                otherItemsTable.Append($"| {item.m_shared.m_teleportable}{Environment.NewLine}");
                otherItemsTable.Append($"| {(item.m_shared.m_value != 0 ? item.m_shared.m_value : "None")}{Environment.NewLine}");
                otherItemsTable.Append($"| {item.m_shared.m_maxStackSize}{Environment.NewLine}");
                otherItemsTable.Append($"| {item.m_shared.m_maxQuality}{Environment.NewLine}");
                otherItemsTable.Append($"|-{Environment.NewLine}");
            }

            int i = 0;
            foreach (var itemGameObject in objectDB.m_items)
            {
                var item = itemGameObject.GetComponentInChildren<ItemDrop>().m_itemData;

                var itemType = item.m_shared.m_itemType;

                if (itemType == ItemDrop.ItemData.ItemType.Consumable)
                {
                    ConsumableItemInfo(itemGameObject, item, ref i);
                }
                else if (itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon ||
                    itemType == ItemDrop.ItemData.ItemType.Bow ||
                    itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon ||
                    itemType == ItemDrop.ItemData.ItemType.Torch)
                {
                    WeaponItemInfo(itemGameObject, item, ref i);
                }
                else if (itemType == ItemDrop.ItemData.ItemType.Shield)
                {
                    ShieldItemInfo(itemGameObject, item, ref i);
                }
                else if (itemType == ItemDrop.ItemData.ItemType.Helmet ||
                    itemType == ItemDrop.ItemData.ItemType.Chest ||
                    itemType == ItemDrop.ItemData.ItemType.Legs ||
                    itemType == ItemDrop.ItemData.ItemType.Shoulder)
                {
                    ArmorItemInfo(itemGameObject, item, ref i);
                }
                else if (itemType == ItemDrop.ItemData.ItemType.Ammo)
                {
                    AmmoItemInfo(itemGameObject, item, ref i);
                }
                else
                {
                    OtherItemInfo(itemGameObject, item, ref i);
                }
            }

            consumablesTable.Append("|}");
            UnityEngine.Debug.LogWarning(consumablesTable);

            weaponsTable.Append("|}");
            UnityEngine.Debug.LogWarning(weaponsTable);

            shieldsTable.Append("|}");
            UnityEngine.Debug.LogWarning(shieldsTable);

            armorsTable.Append("|}");
            UnityEngine.Debug.LogWarning(armorsTable);

            ammoTable.Append("|}");
            UnityEngine.Debug.LogWarning(ammoTable);

            otherItemsTable.Append("|}");
            UnityEngine.Debug.LogWarning(otherItemsTable);
        }
    }
}
