using BettingHouse.Domain.Entity;
using BettingHouse.Dtos;
using System.Collections.Generic;

namespace BettingHouse.Domain
{
    public interface IDomainService
    {
        string CreateRoulette();
        IEnumerable<RouletteGame> ListRouletteGames();
        string Startwager(string id);
        void CreateWager(string id, Wager wager);
        RouletteDto ClosedRoulette(string id);
    }
}
