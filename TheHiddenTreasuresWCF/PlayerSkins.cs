using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TheHiddenTreasuresWCF
{
    [DataContract]
    public class PlayerSkins
    {
        [DataMember]
        public int currentSkin { get; set; }

        [DataMember]
        public List<int> skins { get; set; }

        public PlayerSkins(int currentSkin, List<int> skins)
        {
            this.currentSkin = currentSkin;
            this.skins = skins;
        }
    }
}
