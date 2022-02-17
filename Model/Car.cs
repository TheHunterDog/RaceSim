using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Car : IEquipment
    {
        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Performance = performance;
            Speed = speed;
            this.isBroken = isBroken;
        }

        public int Quality { get ; set ; }
        public int Performance { get ; set ; }
        public int Speed { get ; set ; }
        public bool isBroken { get ; set ; }
    }
}
