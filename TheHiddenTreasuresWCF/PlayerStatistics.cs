using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TheHiddenTreasuresWCF
{
    [DataContract]
    public class PlayerStatistics
    {
        [DataMember]
        public string username { get; set; }

        [DataMember]
        public int gamesPlayed { get; set; }

        [DataMember]
        public int gamesWon { get; set; }

        [DataMember]
        public int minTime { get; set; }

        [DataMember]
        public int coins { get; set; }

        public PlayerStatistics(string username, int gamesPlayed, int gamesWon, int minTime, int coins)
        {
            this.username = username;
            this.gamesPlayed = gamesPlayed;
            this.gamesWon = gamesWon;
            this.minTime = minTime;
            this.coins = coins;
        }
    }
}
