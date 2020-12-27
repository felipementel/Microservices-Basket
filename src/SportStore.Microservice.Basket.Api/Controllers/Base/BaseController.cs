using Microsoft.AspNetCore.Mvc;
using SportStore.Microservice.Basket.Application.DTO.Base;
using SportStore.Microservice.Basket.Application.Interfaces;
using System;
using System.Diagnostics;

namespace SportStore.Microservice.Basket.Api.Controllers.Base
{
    public abstract class BaseController<TDTO, Tid> : ControllerBase
            where TDTO : BaseDTOEntity<Tid>
    {
        protected readonly IBaseAppService<TDTO, Tid> _appService;

        public readonly Stopwatch sw;
        protected BaseController(IBaseAppService<TDTO, Tid> appService)
        {
            _appService = appService;

            sw = new Stopwatch();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string TempoProcessamento(Stopwatch stopwatch)
        {
            return $"{TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).Seconds} segundos e " +
                        $"{TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).Milliseconds} milessegundos";
        }
    }
}
