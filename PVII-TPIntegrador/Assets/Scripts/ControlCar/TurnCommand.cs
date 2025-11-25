using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ControlCar
{
    public class TurnCommand
    {
        private ContextCar car;
        private float turn;

        public TurnCommand(ContextCar car, float turnValue)
        {
            car = car;
            turn = turnValue;
        }

        public void Execute()
        {
            car.ApplyMovement(0, turn);
        }
    }
}