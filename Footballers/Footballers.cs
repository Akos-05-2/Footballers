using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers
{
    class Footballers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public string Nationality { get; set; }
        public int Goals { get; set; }

        public override string ToString()
        {
            return $"Footballer: <Id: {Id}, FistName: {FirstName}, LastName: {LastName}, Team: {Team}, Nationality: {Nationality}, Goals: {Goals}>";
        }
        public string ToCSVLine()
        {
            return $"{Id}, {FirstName}, {LastName}" + $"{Team}, {Nationality}, {Goals}";
        }

        public static Footballers CreateFromLine(string line, char separator = ',')
        {
            string[] values = line.Split(separator);
            return new Footballers()
            {
                Id = int.Parse(values[0]),
                FirstName = values[1],
                LastName = values[2],
                Team = values[3],
                Nationality = values[4],
                Goals = int.Parse(values[5])
            };
        }
    }
}
