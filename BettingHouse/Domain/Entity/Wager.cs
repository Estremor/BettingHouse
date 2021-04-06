using System;

namespace BettingHouse.Domain.Entity
{
    [Serializable]
    public class Wager
    {
        public string UserId { get; set; }
        public int Numbert { get; set; }
        public decimal BetValue { get; set; }
    }
}
