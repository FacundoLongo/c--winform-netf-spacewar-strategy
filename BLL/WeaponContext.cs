using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class WeaponContext
    {
        private IWeaponStrategy _current = new ShortCannonStrategy();

        public void SelectByDistance(double km)
        {
            if (km < 10) _current = new ShortCannonStrategy();
            else if (km <= 50) _current = new UltrasonicStrategy();
            else if (km <= 200) _current = new BionicLaserStrategy();
            else _current = new NoWeaponStrategy();
        }

        public Shot Execute(Target t, int sessionId, ShotRepository repo)
        {
            bool hit = _current.Fire(t);

            var shot = new Shot
            {
                Target = t,
                Weapon = _current.Type,   // puede ser None
                Hit = hit,
                Timestamp = DateTime.Now
            };

            // >>> solo persiste si hay arma válida <<<
            if (_current.Type != WeaponType.None)
                repo.Save(shot, sessionId);

            return shot;
        }

    }

    internal class NoWeaponStrategy : IWeaponStrategy
    {
        public WeaponType Type => WeaponType.None;
        public bool Fire(Target _) => false;
    }
}

