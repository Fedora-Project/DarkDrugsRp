using BrokeProtocol.API;
using BrokeProtocol.Entities;
using BrokeProtocol.Managers;
using BrokeProtocol.Required;
using BrokeProtocol.Utility;
using DarkDrugRP.Json;
using System.Collections.Generic;
using UnityEngine;
using static DarkDrugRP.Json.Config;

namespace DarkDrugRP.Event
{
    internal class Interact
    {
        public int percentage = LoadConfig().LoosingDirtyMoneyPercentage;
        public List<ShConsumable> Drugs = new List<ShConsumable>();
        public Dictionary<ShPlayer, string> chooses = new Dictionary<ShPlayer, string>();

        [CustomTarget]
        public void DirtyMoneyDealer(ShEntity target, ShPlayer player)
        {
            player.svPlayer.SendGameMessage("&4plugin fait par la fedora team");
            LabelID[] lol = new LabelID[] { new LabelID(LoadConfig().exchange_option + percentage + "%", "") };
            player.svPlayer.SendOptionMenu(LoadConfig().panel_name, player.ID, "dealfedora", lol, new LabelID[] { new LabelID(LoadConfig().exchange_action, "") });
        }

        [CustomTarget]
        public void DrugDealer(ShEntity target, ShPlayer player)
        {
            player.svPlayer.SendGameMessage("&4plugin fait par la fedora team");
            List<LabelID> lol = new List<LabelID>();
            foreach(ShConsumable drug in Drugs)
            {
                lol.Add(new LabelID(drug.itemName, drug.itemName));
            }
            player.svPlayer.SendOptionMenu(LoadConfig().DrugDeal_Menu, player.ID, "dealdrugfedora", lol.ToArray(), new LabelID[] { new LabelID(LoadConfig().DrugDeal_Action, "") });
        }

        [Target(GameSourceEvent.ManagerStart, ExecutionMode.Event)]
        public void OnStart(SvManager manager) 
        {
            string[] drugs = { "Heroin", "Cocaine", "Weed", "Meth" };
            foreach (ShEntity Drug in SceneManager.Instance.entityCollection.Values)
            {
                foreach(string d in drugs)
                {
                    if(Drug.name == d && Drug as ShConsumable)
                    {
                        Drugs.Add((ShConsumable)Drug);
                    }
                }
            }
        }

        [Target(GameSourceEvent.PlayerOptionAction, ExecutionMode.Event)]
        public void dealmenu(ShPlayer player, int targetID, string menuID, string optionID, string actionID)
        {
            if (menuID == "dealfedora")
            {
                player.svPlayer.SendInputMenu(LoadConfig().panel_name, player.ID, "dealfedora", UnityEngine.UI.InputField.ContentType.IntegerNumber);
            }
            else if (menuID == "dealdrugfedora")
            {
                if (chooses.ContainsKey(player))
                {
                    chooses.Remove(player);
                }
                chooses.Add(player, optionID);
                player.svPlayer.SendInputMenu(LoadConfig().panel_name, player.ID, "dealdrugfedora", UnityEngine.UI.InputField.ContentType.IntegerNumber);
            }
        }

        [Target(GameSourceEvent.PlayerSubmitInput, ExecutionMode.Event)]
        public void OnSubmitInput(ShPlayer player, int targetID, string menuID, string input)
        {
            if (menuID == "dealfedora")
            {
                if (input.TryParseInt() >= 10)
                {
                    if (player.MyItemCount("DirtyMoney".GetPrefabIndex()) >= input.TryParseInt())
                    {
                        int total = (percentage * input.TryParseInt()) / 100;
                        player.TransferItem(DeltaInv.RemoveFromMe, "DirtyMoney".GetPrefabIndex(), input.TryParseInt(), true);
                        player.TransferMoney(DeltaInv.AddToMe, total, true);
                        player.svPlayer.SendGameMessage(LoadConfig().exchange_finish + " + " + total + " Money !");
                    }
                    else
                    {
                        player.svPlayer.SendGameMessage(LoadConfig().exchange_fail2);
                    }
                }
                else
                {
                    player.svPlayer.SendGameMessage(LoadConfig().exchange_fail1);
                }
            }
            else if (menuID == "dealdrugfedora")
            {
                if (input.TryParseInt() >= 1)
                {
                    chooses.TryGetValue(player, out var s);
                    switch (s)
                    {
                        case "Cocaine":
                            if (player.ItemCount(player.myItems, s.GetPrefabIndex()) >= input.TryParseInt())
                            {
                                player.TransferItem(DeltaInv.RemoveFromMe, s.GetPrefabIndex(), input.TryParseInt(), true);
                                player.TransferItem(DeltaInv.AddToMe, "DirtyMoney".GetPrefabIndex(), LoadConfig().PricesCocaine * input.TryParseInt());
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_finish + " + " + LoadConfig().PricesCocaine * input.TryParseInt() + " DirtyMoney !");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_fail2);
                            }
                            break;
                        case "Heroin":
                            if (player.ItemCount(player.myItems, s.GetPrefabIndex()) >= input.TryParseInt())
                            {
                                player.TransferItem(DeltaInv.RemoveFromMe, s.GetPrefabIndex(), input.TryParseInt(), true);
                                player.TransferItem(DeltaInv.AddToMe, "DirtyMoney".GetPrefabIndex(), LoadConfig().PricesHeroin * input.TryParseInt());
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_finish + " + " + LoadConfig().PricesHeroin * input.TryParseInt() + " DirtyMoney !");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_fail2);
                            }
                            break;
                        case "Weed":
                            if (player.ItemCount(player.myItems, s.GetPrefabIndex()) >= input.TryParseInt())
                            {
                                player.TransferItem(DeltaInv.RemoveFromMe, s.GetPrefabIndex(), input.TryParseInt(), true);
                                player.TransferItem(DeltaInv.AddToMe, "DirtyMoney".GetPrefabIndex(), LoadConfig().PricesWeed * input.TryParseInt());
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_finish + " + " + LoadConfig().PricesWeed * input.TryParseInt() + " DirtyMoney !");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_fail2);
                            }
                            break;
                        case "Meth":
                            if (player.ItemCount(player.myItems, s.GetPrefabIndex()) >= input.TryParseInt())
                            {
                                player.TransferItem(DeltaInv.RemoveFromMe, s.GetPrefabIndex(), input.TryParseInt(), true);
                                player.TransferItem(DeltaInv.AddToMe, "DirtyMoney".GetPrefabIndex(), LoadConfig().PricesWeed * input.TryParseInt());
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_finish + " + " + LoadConfig().PricesWeed * input.TryParseInt() + " DirtyMoney !");
                            }
                            else
                            {
                                player.svPlayer.SendGameMessage(LoadConfig().exchange_fail2);
                            }
                            break;
                    }
                }
                else
                {
                    player.svPlayer.SendGameMessage(LoadConfig().exchange_fail1);
                }
            }
        }
    }
}
