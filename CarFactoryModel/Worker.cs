using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFactory
{
    /// <summary>
    /// Исполнитель, выполняющий заказы клиентов
    /// </summary>
    public class Worker
    {
        public int Id { get; set; }

		[Required]
		public string WorkerName { get; set; }

		[ForeignKey("WorkerId")]
        public virtual List<Booking> Bookings { get; set; }
	}
}
