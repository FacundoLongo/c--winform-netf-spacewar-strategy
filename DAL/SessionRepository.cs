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

        public int GetHighScore(string nick)
        {
            const string sql = @"
            SELECT ISNULL(MAX(FinalScore),0)
            FROM   GameSession  s
            JOIN   Player       p ON p.PlayerId = s.PlayerId
            WHERE  p.NickName = @nick AND s.FinalScore IS NOT NULL";
            using (var cn = new SqlConnection(DbContext.Cs))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nick", nick);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public (int Games, int Hits, int Shots, double Accuracy, int HighScore)
               GetStats(string nick)
        {
            using (SqlConnection cn = new SqlConnection(DbContext.Cs))
            {
                const string sql = "SELECT * FROM vw_PlayerStats WHERE NickName = @n";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@n", nick);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read()) return (0, 0, 0, 0, 0);

                        return (
                            dr.GetInt32(dr.GetOrdinal("GamesPlayed")),
                            dr.GetInt32(dr.GetOrdinal("TotalHits")),
                            dr.GetInt32(dr.GetOrdinal("TotalShots")),
                            Convert.ToDouble(dr["AccuracyPct"]),
                            GetHighScore(nick)
                        );
                    }
                }
            }
        }

        public DataTable GetStatsTable()
        {
            using (SqlConnection cn = new SqlConnection(DbContext.Cs))
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_PlayerStats", cn))
            {
                DataTable tbl = new DataTable("vw_PlayerStats");
                da.Fill(tbl);
                return tbl;
            }
        }
    }
}
