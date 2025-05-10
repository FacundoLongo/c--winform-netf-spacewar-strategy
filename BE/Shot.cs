using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Shot
    {
        public int Id { get; set; }
        public Target Target { get; set; }
        public WeaponType Weapon { get; set; }
        public bool Hit { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
