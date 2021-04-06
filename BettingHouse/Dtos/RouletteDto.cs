using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingHouse.Dtos
{
    public class RouletteDto
    {
        public RouletteDto()
        {
            winningUser = new Dictionary<string, decimal>();
            waggers = new List<BettingDto>();
        }
        public decimal winninNumber { get; set; }
        public Dictionary<string, decimal> winningUser { get; set; }
        public List<BettingDto> waggers { get; set; }
    }
}
