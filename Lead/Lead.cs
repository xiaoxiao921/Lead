using BepInEx;
using Lead.Items.Lead;
using Lead.Util;

namespace Lead
{
    [BepInDependency(ValheimLib.ValheimLib.ModGuid)]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class Lead : BaseUnityPlugin
    {
        public const string ModGuid = "iDeathHD." + ModName;
        private const string ModName = "Lead";
        private const string ModVer = "0.0.3";

        internal static Lead Instance { get; private set; }

        private void Awake()
        {
            Log.Init(Logger);
            //Util.Debug.Init();

            AssetHelper.Init();

            ItemData.Init();

            Instance = this;
        }
    }
}