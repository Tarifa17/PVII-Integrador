using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FishNet.Object;

namespace Assets.Scripts.ControlCar
{
    /// <summary>
    /// Lee inputs del usuario y los convierte en Comandos.
    /// Además asigna la Strategy al auto.
    /// </summary>
    public class InputHandler : NetworkBehaviour
    {
        private CommandInvoker invoker;
        private ContextCar car;

        private void Awake()
        {
            invoker = new CommandInvoker();
        }

        public override void OnStartClient()
        {
            car = GetComponent<ContextCar>();

            // Asignamos Strategy aquí
            car.MovementStrategy = new ArcadeMovementStrategy(
                acc: 4f,
                vmax: 7f,
                giro: 2.5f,
                grip: 6f
            );
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;

            //Input del jugador
            float accel = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            // Creamos comandos separados
            invoker.AddCommand(new AccelerateCommand(car, accel));
            invoker.AddCommand(new TurnCommand(car, turn));

            invoker.ExecuteCommands();
        }
    }
}
