using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ControlCar
{
    public interface IMovementStrategy
    {
        void Move(ContextCar car, float accelInput, float turnInput);
    }
}
