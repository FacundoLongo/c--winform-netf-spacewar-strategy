using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IShotRepository
    {
        void Save(Shot shot, int sessionId);
    }
}