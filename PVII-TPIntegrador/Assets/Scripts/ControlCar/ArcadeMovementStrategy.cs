using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ControlCar
{
    /// <summary>
    /// Estrategia de movimiento estilo arcade.
    /// Controla aceleración, giro y agarre.
    /// </summary>
    public class ArcadeMovementStrategy: IMovementStrategy
    {
        private float aceleracion;
        private float velocidadMaxima;
        private float fuerzaGiro;
        private float agarre;

        public ArcadeMovementStrategy(float acc, float vmax, float giro, float grip)
        {
            //Datos compartidos para otras clases
            aceleracion = acc;
            velocidadMaxima = vmax;
            fuerzaGiro = giro;
            agarre = grip;
        }

        public void Move(ContextCar car, float accelInput, float turnInput)
        {
            Rigidbody2D rig = car.Rig;

            // Aceleraracion frontal
            rig.AddForce(car.transform.up * accelInput * aceleracion);

            // Limitar velocidad máxima
            rig.linearVelocity = Vector2.ClampMagnitude(rig.linearVelocity, velocidadMaxima);

            // Giro dependiente de la velocidad
            float velocidadActual = rig.linearVelocity.magnitude;
            float giro = turnInput * fuerzaGiro * (velocidadActual / velocidadMaxima);

            //Rotacion final
            rig.MoveRotation(rig.rotation - giro);

            // Reducción del patinaje (agarre)
            Vector2 velocidadLateral = Vector2.Dot(rig.linearVelocity, car.transform.right) * car.transform.right;
            Vector2 velocidadAdelante = Vector2.Dot(rig.linearVelocity, car.transform.up) * car.transform.up;

            rig.linearVelocity = velocidadAdelante + velocidadLateral * (1f / agarre);
        }
    }
}
