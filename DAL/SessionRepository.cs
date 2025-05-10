using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SessionRepository
    {
        public int StartSession(int playerId)
        {
            using (SqlConnection cn = new SqlConnection(DbContext.Cs))
            {
                using (SqlCommand cmd = new SqlCommand("spStartSession", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlayerId", playerId);
                    var pOut = cmd.Parameters.Add("@OutSessId", SqlDbType.Int);
                    pOut.Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return (int)pOut.Value;
                }
            }
        }

        public void FinishSession(int sessionId, int score, int lives)
        {
            using (SqlConnection cn = new SqlConnection(DbContext.Cs))
            {
                using (SqlCommand cmd = new SqlCommand("spFinishSession", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SessionId", sessionId);
                    cmd.Parameters.AddWithValue("@FinalScore", score);
                    cmd.Parameters.AddWithValue("@FinalLives", lives);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
