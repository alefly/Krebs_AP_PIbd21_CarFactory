namespace CarFactoryService.ViewModels
{
    public class BookingView
    {
        public int Id { get; set; }

        public int ConsumerId { get; set; }

        public string ConsumerName { get; set; }

        public int CommodityId { get; set; }

        public string CommodityName { get; set; }

        public int? WorkerId { get; set; }

        public string WorkerName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateWork { get; set; }
    }
}
