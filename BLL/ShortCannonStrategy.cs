using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class ShortCannonStrategy : IWeaponStrategy
    {
        public WeaponType Type => WeaponType.ShortCannon;
        public bool Fire(Target target) => target.DistanceKm < 10;
    }
}