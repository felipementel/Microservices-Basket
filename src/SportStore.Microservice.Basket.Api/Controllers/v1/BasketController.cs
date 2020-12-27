using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Microservice.Basket.Api.Controllers.Base;
using SportStore.Microservice.Basket.Application.DTO;
using SportStore.Microservice.Basket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SportStore.Micrsoservice.Basket.Api.Controllers.v1
{
    //[Authorize(Roles = "Salesman")]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController : BaseController<BasketDTO, string>
    {
        public BasketController(IBasketAppService appService) : base(appService) { }

        [HttpPost("AddProduct")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddProduct([FromBody] BasketDTO basketDTO)
        {
            if (ModelState.IsValid)
            {
                basketDTO.UserId = User.FindFirst("sub")?.Value;
                var retorno = await _appService.AddAsync(basketDTO);

                return retorno != null
                    ? Ok(new
                    {
                        success = true,
                        data = retorno
                    })
                    : (IActionResult)NotFound();
            }

            return BadRequest(basketDTO);
        }

        [HttpGet("GetAllItemsInBasket")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<BasketDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetAllItemsInBasket()
        {
            sw.Start();

            var email = User.FindFirst("sub")?.Value;

            var retorno = await _appService.GetAllAsync();

            sw.Stop();

            return retorno.Any()
                ? Ok(new
                {
                    success = true,
                    data = retorno,
                    tempoProcessamento = TempoProcessamento(sw)
                })
                : (IActionResult)NotFound();
        }

        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(string id, [FromBody] BasketDTO basketDTO)
        {
            sw.Start();

            var retorno = await _appService.UpdateAsync(id, basketDTO);

            sw.Stop();

            if (retorno != null && !retorno.ValidationResult.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    data = retorno.ValidationResult.ToString(),
                    tempoProcessamento = TempoProcessamento(sw)
                });
            }

            return Ok(new
            {
                success = true,
                data = retorno,
                tempoProcessamento = TempoProcessamento(sw)
            });
        }

        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                sw.Start();

                var retorno = await _appService.RemoverAsync(id);

                sw.Stop();

                return Ok(new
                {
                    success = true,
                    data = retorno.ToString(),
                    tempoProcessamento = TempoProcessamento(sw)
                });
            }
            return BadRequest(id);
        }
    }
}