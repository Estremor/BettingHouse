using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingHouse.Dtos
{
    public class BettingDto
    {
        public string UserId { get; set; }
        public int Numbert { get; set; }
        public decimal BetValue { get; set; }
    }
}
