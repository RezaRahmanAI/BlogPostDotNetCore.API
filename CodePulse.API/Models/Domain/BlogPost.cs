namespace CodePulse.API.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string UrlHandle { get; set; }
        public string FeatureImgUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
    }
}
