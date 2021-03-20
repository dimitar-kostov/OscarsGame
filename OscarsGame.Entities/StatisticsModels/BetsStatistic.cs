using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Entities.StatisticsModels
{
    public class BetsStatistic
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CategoryTitle { get; set; }
        public string MovieTitle { get; set; }
        public bool IsWinner { get; set; }
    }
}
