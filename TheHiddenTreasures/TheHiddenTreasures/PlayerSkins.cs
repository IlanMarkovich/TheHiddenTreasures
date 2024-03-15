using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHiddenTreasures
{
    public class PlayerSkins
    {
        public string username { get; set; }
        public ServiceReference1.PlayerSkins skins;

        public PlayerSkins(string username, ServiceReference1.PlayerSkins skins)
        {
            this.username = username;
            this.skins = skins;
        }
    }
}
