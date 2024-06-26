﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TheHiddenTreasuresWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool ValidateUser(User user);

        [OperationContract]
        bool RegisterUser(User user);

        bool HasUsername(string username);

        [OperationContract]
        bool UpdateStatistics(string username, bool didWin, int time, int coins);

        [OperationContract]
        List<PlayerStatistics> GetPlayerStatistics();

        PlayerStatistics GetAverageStatstics();
        PlayerStatistics GetSumStatistics();
        PlayerStatistics GetMaxStatistics();
        PlayerStatistics GetMinStatistics();

        [OperationContract]
        int GetPlayerCoins(string username);

        [OperationContract]
        int GetPlayerCurrentSkin(string username);

        [OperationContract]
        bool UpdatePlayerCurrentSkin(string username, int skin);

        [OperationContract]
        bool BuyPlayerSkin(string username, int skin, int price);

        [OperationContract]
        bool AddItem(string username, int item);

        [OperationContract]
        List<int> GetUserItems(string username);

        [OperationContract]
        bool DeleteUser(string username);

        [OperationContract]
        PlayerSkins GetPlayerSkins(string username);
    }
}
