using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingHouse.Domain;
using BettingHouse.Domain.Entity;
using BettingHouse.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BettingHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BettingController : ControllerBase
    {
        private readonly IDomainService service;
        private readonly IValidator<BettingModel> validator;

        public BettingController(IDomainService domainService, IValidator<BettingModel> validator)
        {
            service = domainService;
            this.validator = validator;
        }
        // GET: api/<BettingController>
        [HttpGet(Name = nameof(BettingController.List))]
        public IActionResult List()
        {
            try
            {
                var list = service.ListRouletteGames();
                return Ok(list);
            }
            catch (Exception e)
            {
                return StatusCode(405, e.Message);
            }
        }

        // GET api/<BettingController>/5
        [HttpPost("{id}", Name = nameof(BettingController.HabilitateWager))]
        public IActionResult HabilitateWager(string id)
        {
            try
            {
                service.Startwager(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<BettingController>
        [HttpPost(Name = nameof(BettingController.Create))]
        public IActionResult Create()
        {
            try
            {
                var result = service.CreateRoulette();
                return StatusCode(201, result);
            }
            catch (Exception e)
            {
                return StatusCode(405, e.Message);
            }
        }

        // PUT api/<BettingController>/5
        [HttpPut("{id}", Name = nameof(BettingController.CreateWager))]
        public IActionResult CreateWager([FromHeader(Name = "UserToken")] string UserToken, string id, BettingModel model)
        {
            try
            {
                List<string> errors = GetErrorsModel(model);
                if (GetErrorsModel(model) != null)
                {
                    return StatusCode(500, errors);
                }
                Wager wager = new Wager { BetValue = model.Amount, Numbert = model.Number, UserId = UserToken };
                service.CreateWager(id, wager);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<BettingController>/5
        [HttpDelete("{id}", Name = nameof(BettingController.ClosedBetting))]
        public IActionResult ClosedBetting(string id)
        {
            try
            {
                Dtos.RouletteDto resutlRoulette = service.ClosedRoulette(id);
                return Ok(resutlRoulette);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private List<string> GetErrorsModel(BettingModel model)
        {
            FluentValidation.Results.ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                return result.Errors.Select(x => x.ErrorMessage).ToList();
            }
            return null;
        }
    }
}
