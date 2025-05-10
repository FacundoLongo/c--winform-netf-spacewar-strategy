using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class BionicLaserStrategy : IWeaponStrategy
    {
        public WeaponType Type => WeaponType.BionicLaser;
        public bool Fire(Target target) =>
            target.DistanceKm > 50 && target.DistanceKm <= 200;
    }
}