using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ControlCar
{
    /// <summary>
    /// Comando que representa la acción de acelerar (o frenar si es negativo).
    /// Encapsula el valor del input vertical.
    /// </summary>
    public class AccelerateCommand : ICommand
    {
        private ContextCar car;
        private float accelValue;
        //private float turnValue;

        public AccelerateCommand(ContextCar car, float accel)
        {
            this.car = car;
            this.accelValue = accel;
            //this.turnValue = turn;
        }

        public void Execute()
        {
            car.ApplyAcceleration(accelValue);
        }
    }
}
