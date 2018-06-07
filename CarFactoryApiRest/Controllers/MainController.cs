using CarFactoryApiRest.Services;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarFactoryApiRest.Controllers
{
    public class MainController : ApiController
	{
		private readonly IMain _service;

		public MainController(IMain service)
		{
			_service = service;
		}

		[HttpGet]
		public IHttpActionResult GetList()
		{
			var list = _service.GetList();
			if (list == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(list);
		}

		[HttpPost]
		public void CreateBooking(BindingBooking model)
		{
			_service.CreateBooking(model);
		}

		[HttpPost]
		public void TakeBookingInWork(BindingBooking model)
		{
			_service.TakeBookingInWork(model);
		}

		[HttpPost]
		public void FinishBooking(BindingBooking model)
		{
			_service.FinishBooking(model.Id);
		}

		[HttpPost]
		public void PayBooking(BindingBooking model)
		{
			_service.PayBooking(model.Id);
		}

		[HttpPost]
		public void PutIngridientOnStorage(BindingStorageIngridients model)
		{
			_service.PutIngridientOnStorage(model);
		}

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

	}
}