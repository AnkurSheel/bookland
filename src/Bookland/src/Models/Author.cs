namespace Bookland.Models
{
    public class Author
    {
        public Author(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; }

        public string Link { get; }
    }
}