using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHiddenTreasures
{
    internal class PlayerStatistics
    {
        public string username { get; set; }
        public int gamesPlayed { get; set; }
        public int gamesWon { get; set; }
        public int minTime { get; set; }

        public PlayerStatistics(string username, int gamesPlayed, int gamesWon, int minTime)
        {
            this.username = username;
            this.gamesPlayed = gamesPlayed;
            this.gamesWon = gamesWon;
            this.minTime = minTime;
        }
    }
}
