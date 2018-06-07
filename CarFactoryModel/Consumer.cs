using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFactory
{
    /// <summary>
    /// Клиент магазина
    /// </summary>
    public class Consumer
    {
        public int Id { get; set; }

		[Required]
        public string ConsumerName { get; set; }

		[ForeignKey("ConsumerId")]
		public virtual List<Booking> Bookings { get; set; }
    }
}
