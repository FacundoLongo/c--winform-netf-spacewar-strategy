using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class UltrasonicStrategy : IWeaponStrategy
    {
        public WeaponType Type => WeaponType.UltrasonicCannon;
        public bool Fire(Target target) =>
            target.DistanceKm >= 10 && target.DistanceKm <= 50;
    }
}