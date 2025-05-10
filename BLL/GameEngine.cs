using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;

namespace BLL
{
    public class GameEngine
    {
        //---------------- DAL internos ----------------
        private readonly ShotRepository _shotRepo = new ShotRepository();
        private readonly PlayerRepository _playerRepo = new PlayerRepository();
        private readonly SessionRepository _sessionRepo = new SessionRepository();

        //---------------- estado ----------------
        private readonly List<Target> _targets = new List<Target>();
        private readonly Random _rnd = new Random();
        private readonly WeaponContext _ctx = new WeaponContext();

        private int _sessionId;                       

        public double KmPerPixel { get; set; } = 1;
        public int Lives { get; private set; } = 3;
        public int Hits { get; private set; }
        public IReadOnlyList<Target> Targets => _targets.AsReadOnly();

        public event Action StateChanged;

        public void StartGame(string nick)
        {
            int playerId = _playerRepo.EnsurePlayer(nick);
            _sessionId = _sessionRepo.StartSession(playerId);
            Lives = 3;
            Hits = 0;
            _targets.Clear();
            StateChanged?.Invoke();
        }

        public void EndGame()
        {
            _sessionRepo.FinishSession(_sessionId, Hits, Lives);
        }

        public void Tick(int pixels)
        {
            foreach (var t in _targets.ToList())
            {
                t.DistanceKm -= pixels * KmPerPixel;
                if (t.DistanceKm <= 0) { _targets.Remove(t); Lives--; }
            }
            StateChanged?.Invoke();
        }

        public void Spawn(int lane, double kmFromBase)
        {
            _targets.Add(new Target
            {
                Id = _rnd.Next(),
                Lane = lane,
                DistanceKm = kmFromBase
            });
        }

        public Shot Fire(int lane)
        {
            var tgt = _targets.Where(t => t.Lane == lane)
                              .OrderBy(t => t.DistanceKm)
                              .FirstOrDefault();
            if (tgt == null) return null;

            _ctx.SelectByDistance(tgt.DistanceKm);

            if (_ctx.CurrentWeapon == WeaponType.None)
                return null;

            var shot = _ctx.Execute(tgt, _sessionId, _shotRepo);
            return shot;
        }


        public void RegisterHit(int targetId)
        {
            var tgt = _targets.FirstOrDefault(t => t.Id == targetId);
            if (tgt == null) return;

            _targets.Remove(tgt);
            Hits++;
            StateChanged?.Invoke();
        }

    }
}
