using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;
using MoveSystem;

namespace MoveSystem
{
    public interface IMoveCommandProvider<TPiece> where TPiece : class, IAction<TPiece>
    {
        List<IMoveCommand<TPiece>> Commands();
    }
}