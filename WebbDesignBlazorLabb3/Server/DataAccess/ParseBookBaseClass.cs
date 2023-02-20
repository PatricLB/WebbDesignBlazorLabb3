namespace WebbDesignBlazorLabb3.Server.DataAccess;

public class ParseBookBaseClass
{
    public class Root
    {
        public List<Item> items { get; set; }
    }
    public class Item
    {
        public VolumeInfo volumeInfo { get; set; }
        public SearchInfo searchInfo { get; set; }
    }
    public class VolumeInfo
    {
        public string title { get; set; }
        public List<string> authors { get; set; }
        public string description { get; set; }
        public int pageCount { get; set; }
        public ImageLinks imageLinks { get; set; }
    }

    public class ImageLinks
    {
        public string thumbnail { get; set; }
    }

    public class SearchInfo
    {
        public string textSnippet { get; set; }
    }


}
