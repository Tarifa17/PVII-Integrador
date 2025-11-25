using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ControlCar
{
    public class AccelerateCommand : ICommand
    {
        private ContextCar car;
        private float accelValue;
        private float turnValue;

        public AccelerateCommand(ContextCar car, float accel, float turn)
        {
            this.car = car;
            this.accelValue = accel;
            this.turnValue = turn;
        }

        public void Execute()
        {
            car.ApplyMovement(accelValue, turnValue);
        }
    }
}
