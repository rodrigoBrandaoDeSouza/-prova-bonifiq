namespace ProvaPub.Models
{
    public class EntityList<T>
    {
        public List<T> List { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }

    }
}
