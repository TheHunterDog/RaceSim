using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Car : IEquipment
    {
        #region private_varables
        private static int MINQUALITY = 20;
        private static int MINPERFORMANCE = 20;
        private static int MINSPEED = 20;

        private int _quality;
        private int _performance;
        private int _speed;
        #endregion
        #region props
        public int Quality { get => _quality; set { 
            if(value > MINQUALITY)
                {
                    _quality = value;
                }
            }
        }
        public int Performance { get => _performance ; set
            {
                if(value > MINPERFORMANCE)
                {
                    _performance = value;
                }
            }
        }
        public int Speed { get => _speed ; set
            {
                if(value > MINSPEED)
                {
                    _speed = value;
                }
            }
        }
        public bool isBroken { get ; set ; }
        #endregion
        #region constructor
        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Performance = performance;
            Speed = speed;
            this.isBroken = isBroken;
        }
        #endregion
    }
}
