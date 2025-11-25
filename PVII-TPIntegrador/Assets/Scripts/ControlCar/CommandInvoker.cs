using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ControlCar
{
    public class CommandInvoker
    {
        private Queue<ICommand> commandQueue = new Queue<ICommand>();

        public void AddCommand(ICommand cmd)
        {
            commandQueue.Enqueue(cmd);
        }

        public void ExecuteCommands()
        {
            while (commandQueue.Count > 0)
            {
                ICommand cmd = commandQueue.Dequeue();
                cmd.Execute();
            }
        }
    }
}
