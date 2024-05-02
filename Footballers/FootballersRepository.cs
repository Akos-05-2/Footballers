using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Footballers
{
    class FootballersRepository : IFootballersRepository
    {
        public string Path { get; set; }
        public bool SkipHeader { get; set; } = true;
        public char Separator { get; set; } = ',';

        public List<Footballers> FindAll()
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                List<Footballers> players = new List<Footballers>();
                if (SkipHeader)
                {
                    sr.ReadLine();
                }
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Footballers footballers = Footballers.CreateFromLine(line, Separator);
                    players.Add(footballers);
                }

                return players;
            }
        }
        public Footballers FindById(int id)
        {
            foreach (Footballers footballers in FindAll())
            {
                if (footballers.Id == id)
                {
                    return footballers;
                }
            }
            return null;
        }

        public Footballers Save(Footballers footballer)
        {
            List<Footballers> players = FindAll();
            if (footballer.Id == 0)
            {
                int maxId = 0;
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Id > maxId)
                    {
                        maxId = players[i].Id;
                    }
                }
                footballer.Id = maxId + 1;
                players.Add(footballer);
            }
            else
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Id == footballer.Id)
                    {
                        players[i] = footballer;
                        break;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(Path))
            {
                sw.WriteLine("id,firstname,lastname,team,nationality,goals");

                for (int i = 0; i < players.Count; i++)
                {
                    sw.WriteLine(players[i].ToCSVLine());
                }
            }
                return footballer;
        }

        public void Delete(Footballers footballer)
        {
            List<Footballers> players = FindAll();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Id == footballer.Id)
                {
                    players.RemoveAt(i);
                    break;
                }
            }
            using (StreamWriter sw = new StreamWriter(Path))
            {
                sw.WriteLine("id,firstname,lastname,team,nationality,goals");
                for (int i = 0; i < players.Count; i++)
                {
                    sw.WriteLine(players[i].ToCSVLine());
                }
            }
        }

        public List<Footballers> FindAllByTeamLike(string team)
        {
            return FindAll()
                .Where(player => player.Team.ToLower().Contains(team.ToLower()))
                .ToList();
        }
    }
}
