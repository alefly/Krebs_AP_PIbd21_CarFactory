namespace CarFactory
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StorageIngridient
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int IngridientId { get; set; }

        public int Count { get; set; }

		public virtual Storage Storage { get; set; }

        public virtual Ingridient Ingridient { get; set; }
	}
}
