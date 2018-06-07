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
    public class WorkerController : ApiController
    {
		private readonly IWorker _service;

		public WorkerController(IWorker service)
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

		[HttpGet]
		public IHttpActionResult Get(int id)
		{
			var element = _service.GetElement(id);
			if (element == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(element);
		}

		[HttpPost]
		public void AddElement(BindingWorkers model)
		{
			_service.AddElement(model);
		}

		[HttpPost]
		public void UpdElement(BindingWorkers model)
		{
			_service.UpdElement(model);
		}

		[HttpPost]
		public void DelElement(BindingWorkers model)
		{
			_service.DelElement(model.Id);
		}
	}
}