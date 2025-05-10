using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PlayerRepository
    {
        public int EnsurePlayer(string nick)
        {
            using (SqlConnection cn = new SqlConnection(DbContext.Cs))
            {
                using (SqlCommand cmd = new SqlCommand("spEnsurePlayer", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nick", nick);
                    var pOut = cmd.Parameters.Add("@OutId", SqlDbType.Int);
                    pOut.Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return (int)pOut.Value;
                }
            }
        }
    }
}
