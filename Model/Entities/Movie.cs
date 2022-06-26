namespace Model.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MovieTime { get; set; }
        public string director { get; set; }
        public DateTime Premiere { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
