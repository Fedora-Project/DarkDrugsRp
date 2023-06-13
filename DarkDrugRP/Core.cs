using BrokeProtocol.API;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace DarkDrugRP
{
    public class Core : Plugin
    {
        public Core()
        {
            Info = new PluginInfo("DarkDrugRP", "dgp", "plugin by fedora team, that add drugs hard complexe system, configurable", "https://discord.gg/CW772egmm2");
            Debug.Log("[Fedora] " + " load FedoraCore ...");
            if (!Directory.Exists("./FedoraCore/")) { Directory.CreateDirectory("./FedoraCore/"); Debug.Log("[FedoraCore] " + "Creating Folder waiting pls..."); }
        }
    }
}
