using BettingHouse.Domain.Entity;
using BettingHouse.Dtos;
using BettingHouse.Properties;
using BettingHouse.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BettingHouse.Domain
{
    public class DomainService : IDomainService
    {
        #region Properties
        private readonly IRouletteGameRepository rouletteGameRepo;
        #endregion

        #region Const
        private const int PERCENTAGE_FOR_NUMBER = 5;
        private const decimal PERCENTAGE_FOR_COLOR = 1.8M;
        #endregion

        #region Builds
        public DomainService(IRouletteGameRepository rouletteRepo)
        {
            rouletteGameRepo = rouletteRepo;
        }
        #endregion

        public string CreateRoulette()
        {
            RouletteGame game = new RouletteGame
            {
                Id = Guid.NewGuid().ToString(),
                CreationDate = DateTime.UtcNow,
                IsOpen = false,
            };
            RouletteGame gameResult = rouletteGameRepo.Save(game);

            return gameResult.Id;
        }
        public IEnumerable<RouletteGame> ListRouletteGames()
        {
            List<RouletteGame> games = rouletteGameRepo.List().ToList();
            if (games.Count == 0)
            {
                return new List<RouletteGame>();
            }

            return games;
        }
        public string Startwager(string id)
        {
            RouletteGame game = rouletteGameRepo.Get(id);
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(Resources.InvalidIdRoulette);
            }
            if (game.IsOpen)
            {
                throw new InvalidOperationException(Resources.RouletteIsOpen);
            }
            game.IsOpen = true;
            rouletteGameRepo.Update(game.Id, game);

            return game.Id;
        }
        public void CreateWager(string id, Wager wager)
        {
            var roulette = rouletteGameRepo.Get(id);
            if (roulette == null)
            {
                throw new Exception();
            }
            if (roulette.Wagers is null || roulette.Wagers.Count == 0)
            {
                roulette.Wagers = new List<Wager> { wager };
            }
            else
            {
                roulette.Wagers.Add(wager);
            }

            rouletteGameRepo.Update(id, roulette);
        }
        public RouletteDto ClosedRoulette(string id)
        {
            RouletteGame roulette = rouletteGameRepo.Get(id);
            if (roulette == null || roulette.Wagers == null || roulette.Wagers.Count == 0)
            {
                throw new Exception();
            }
            roulette.IsOpen = false;
            roulette.ClosedDate = DateTime.UtcNow;

            RouletteDto wininningUser = GetWinningUser(ref roulette);
            foreach (var item in roulette.Wagers)
            {
                wininningUser.waggers.Add(new BettingDto { BetValue = item.BetValue, Numbert = item.Numbert, UserId = item.UserId });
            }
            rouletteGameRepo.Update(roulette.Id, roulette);
            return wininningUser;
        }
        private RouletteDto GetWinningUser(ref RouletteGame roulette)
        {
            int winningNumber = GetWinningNumber();
            List<Wager> winningUser = roulette.Wagers.Where(x => x.Numbert == winningNumber).ToList();
            RouletteDto roulettedto = new RouletteDto { winninNumber = winningNumber };
            foreach (var user in winningUser.GroupBy(x => x.UserId))
            {
                roulettedto.winningUser.Add(user.Key, winningNumber <= 36
                    ? user.Sum(x => x.BetValue) * PERCENTAGE_FOR_NUMBER
                    : user.Sum(x => x.BetValue) * PERCENTAGE_FOR_COLOR);
            }
            return roulettedto;
        }
        public int GetWinningNumber()
        {
            int seed = Environment.TickCount;
            Random numero = new Random(seed);
            return numero.Next(0, 38);
        }
    }
}
