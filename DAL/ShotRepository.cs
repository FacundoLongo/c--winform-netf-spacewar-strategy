using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class ShotRepository : IShotRepository
    {
        public void Save(Shot shot, int sessionId)
        {
            using (SqlConnection cn = new SqlConnection(DbContext.Cs))
            {
                using (SqlCommand cmd = new SqlCommand("spRecordShot", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SessionId", sessionId);
                    cmd.Parameters.AddWithValue("@WeaponId", (int)shot.Weapon);
                    cmd.Parameters.AddWithValue("@Distance", shot.Target.DistanceKm);
                    cmd.Parameters.AddWithValue("@Hit", shot.Hit);
                    cmd.Parameters.AddWithValue("@Lane", shot.Target.Lane);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
