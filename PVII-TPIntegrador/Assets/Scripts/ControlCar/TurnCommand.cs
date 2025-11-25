using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ControlCar
{
    /// <summary>
    /// Comando que representa la acción de girar.
    /// Encapsula el input horizontal.
    /// </summary>
    public class TurnCommand: ICommand
    {
        private ContextCar car;
        private float turnValue;

        public TurnCommand(ContextCar car, float turn)
        {
            this.car = car;
            this.turnValue = turn;
        }

        public void Execute()
        {
            car.ApplyTurn(turnValue);
        }
    }
}