using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IParticipant
    {
        String Name { get; set; }
        int Points { get; set; }
        IEquipment equipment { get; set; }
        TeamColors TeamColors { get; set; }
    }
}
