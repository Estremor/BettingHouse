using BettingHouse.Domain.Entity;
using System.Collections.Generic;

namespace BettingHouse.Repository
{
    public interface IRouletteGameRepository
    {
        public RouletteGame Save(RouletteGame game);
        public RouletteGame Get(string Id);
        public IEnumerable<RouletteGame> List();
        public void Update(string Id, RouletteGame game);

    }
}
