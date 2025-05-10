using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public interface IWeaponStrategy
    {
        WeaponType Type { get; }
        bool Fire(Target target);   // true si acierta
    }
}