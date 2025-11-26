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

        // Diccionario que mapea un input a una función creadora de comandos
        private Dictionary<string, Func<ICommand>> commandMap;
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

            // Configuramos el diccionario
            commandMap = new Dictionary<string, Func<ICommand>>()
            {
                { "Vertical", () => new AccelerateCommand(car, Input.GetAxis("Vertical")) },
                { "Horizontal", () => new TurnCommand(car, Input.GetAxis("Horizontal")) }
            };
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;

            DetectInputs();
            invoker.ExecuteCommands();
        }

        /// <summary>
        /// Recorre el diccionario y genera comandos según el input leído.
        /// </summary>
        private void DetectInputs()
        {
            foreach (var entry in commandMap)
            {
                float inputValue = Input.GetAxis(entry.Key);

                // Evitamos crear comandos si no hay input
                if (Mathf.Abs(inputValue) > 0.01f)
                {
                    ICommand cmd = entry.Value.Invoke(); // se crea el comando
                    invoker.AddCommand(cmd); // se encola
                }
            }
        }
    }
}
