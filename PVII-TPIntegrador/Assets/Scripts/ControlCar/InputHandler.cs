using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FishNet.Object;

namespace Assets.Scripts.ControlCar
{
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
                acc: 10f,
                vmax: 8f,
                giro: 3f,
                grip: 4f
            );
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;

            float accel = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            // Creamos comando con ambos inputs
            ICommand moveCommand = new AccelerateCommand(car, accel, turn);
            invoker.AddCommand(moveCommand);

            invoker.ExecuteCommands();
        }
    }
}
