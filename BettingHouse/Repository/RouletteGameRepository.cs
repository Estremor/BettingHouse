using BettingHouse.Domain.Entity;
using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BettingHouse.Repository
{
    public class RouletteGameRepository : IRouletteGameRepository
    {
        #region Properties
        private readonly IEasyCachingProvider cachingDb;
        #endregion

        #region Build
        public RouletteGameRepository(IEasyCachingProviderFactory provider)
        {
            cachingDb = provider.GetCachingProvider(name: ConfigurationValues.DataBaseName);
        }
        #endregion

        #region Methods
        public RouletteGame Save(RouletteGame game)
        {
            string key = ConfigurationValues.Concat(id: game.Id);
            cachingDb.Set(cacheKey: key, cacheValue: game, expiration: TimeSpan.FromHours(ConfigurationValues.ExpirationHorus));

            return game;
        }
        public void Update(string Id, RouletteGame game)
        {
            string key = ConfigurationValues.Concat(id: game.Id);
            cachingDb.Remove(key);

            Save(game);
        }
        public RouletteGame Get(string id)
        {
            string key = ConfigurationValues.Concat(id: id);
            return cachingDb.Get<RouletteGame>(key).Value;
        }
        public IEnumerable<RouletteGame> List()
        {
            return cachingDb.GetByPrefix<RouletteGame>(ConfigurationValues.MemoryKey).Select(x => x.Value.Value);
        }
        #endregion
    }
}
