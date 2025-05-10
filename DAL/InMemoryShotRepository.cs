using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class InMemoryShotRepository : IShotRepository
    {
        public readonly List<Shot> Shots = new List<Shot>();

        public void Save(Shot shot, int sessionId)   // sessionId se ignora
        {
            Shots.Add(shot);
        }
    }
}
