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
    public class ReportController : ApiController
    {
		private readonly IReportService _service;

		public ReportController(IReportService service)
		{
			_service = service;
		}
    
		[HttpPost]
		public IHttpActionResult GetConsumerBookings(ReportBindingModel model)
		{
			var list = _service.GetConsumerBookings(model);
			if (list == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(list);
		}

		[HttpPost]
		public void SaveCommodityPrice(ReportBindingModel model)
		{
			_service.SaveCommodityPrice(model);
		}

		[HttpPost]
		public void SaveStoragesLoad(ReportBindingModel model)
		{
			_service.SaveStoragesLoad(model);
		}

		[HttpPost]
		public void SaveConsumerBookings(ReportBindingModel model)
		{
			_service.SaveConsumerBookings(model);
		}
	}
}
