using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers
{
    interface IFootballersRepository
    {
        List<Footballers> FindAll();
        Footballers FindById(int id);
        Footballers Save(Footballers footballer);
        void Delete(Footballers footballer);
        List<Footballers> FindAllByTeamLike(string team);
    }
}
