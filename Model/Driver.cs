using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Driver : IParticipant
    {
        public Driver(string name, int points, IEquipment equipment, TeamColors teamColors)
        {
            Name = name;
            Points = points;
            this.equipment = equipment;
            TeamColors = teamColors;
        }
        private int _wins = 0;
        public string Name { get ; set ; }
        public int Points { get ; set ; }
        public IEquipment equipment { get ; set ; }
        public TeamColors TeamColors { get ; set ; }
    }
}
