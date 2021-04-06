using System;
using System.Collections.Generic;


namespace BettingHouse.Domain.Entity
{
    [Serializable]
    public class RouletteGame
    {
        public string Id { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public List<Wager> Wagers { get; set; }
    }
}
