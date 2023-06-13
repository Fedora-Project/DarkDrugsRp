using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DarkDrugRP.Json
{
    public class ConfigInfo
    {
        public int LoosingDirtyMoneyPercentage { get; set; } = 50;
        public string panel_name { get; set; } = "&3Exchange DirtyMoney";
        public string exchange_option { get; set; } = "exchange DirtyMoney | percentage of loose : ";
        public string exchange_action { get; set; } = "exchange";
        public string exchange_finish { get; set; } = "&2exchange finish : ";
        public string exchange_fail1 { get; set; } = "&4you can exchange only more than 10 dirtymoney";
        public string exchange_fail2 { get; set; } = "&4you don't have enough dirtymoney";
        public int PricesMeth { get; set; } = 800;
        public int PricesWeed { get; set; } = 200;
        public int PricesCocaine { get; set; } = 450;
        public int PricesHeroin { get; set; } = 600;
        public string DrugDeal_Menu { get; set; } = "&3Seller Drug";
        public string DrugDeal_Action { get; set; } = "Sell Drug";
        public string DrugDeal_Finish { get; set; } = "&2Sell Finish";
        public string DrugDeal_fail1 { get; set; } = "&4you need mores drugs";
        public string DrugDeal_fail2 { get; set; } = "&4ERROR";
    }
    public static class Config
    {
        public static string PathConfig = "./FedoraCore/Config.json";

        public static ConfigInfo LoadConfig()
        {
            if (!File.Exists(PathConfig))
            {
                SetupConfig();
            }
            else
            {
                var json = File.ReadAllText(PathConfig);
                return JsonConvert.DeserializeObject<ConfigInfo>(json);
            }
            ConfigInfo data = new ConfigInfo();
            Debug.Log("default config choosed !");
            return data;
        }

        public static void SetupConfig()
        {
            if (!File.Exists(PathConfig))
            {
                ConfigInfo data = new ConfigInfo();
                using (StreamWriter Sw = File.CreateText(PathConfig))
                {
                    Sw.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
                }
                Debug.Log("creating Fedora Config ...");
            }
        }
    }
}
