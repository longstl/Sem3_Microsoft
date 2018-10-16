using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example3.Model;

namespace Example3.Model
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Headline { get; set; }
        public string Subhead { get; set; }
        public string Dateline { get; set; }
        public string Image { get; set; }
    }
}

public class NewsManager
{
    public static void GetNews(string category, ObservableCollection<NewsItem> newsItems)
    {
        var allItems = getNewsItem();
        var filteredNewsItems = allItems.Where(p => p.Category == category).ToList();
        newsItems.Clear();
        filteredNewsItems.ForEach(p=> newsItems.Add(p));
    }

    private static List<NewsItem> getNewsItem()
    {
        var items = new List<NewsItem>();
        items.Add(new NewsItem { Id = 1, Category = "Financial", Headline = "Lolem Ipsum", Subhead = "doro sit amet", Dateline = "Nunc tristique nec", Image = "Assets/Financial.png" });
        items.Add(new NewsItem { Id = 2, Category = "Financial", Headline = "Etian ac felis viverra", Subhead = "vulputate nisl ac, aliquet nisi", Dateline = "tortor porttitor, eu fermentum ante congue", Image = "Assets/Financial3.png" });
        items.Add(new NewsItem { Id = 3, Category = "Financial", Headline = "Integer sed turpis erat", Subhead = "Sed quis hendrerit lorem, quis interdum dolor", Dateline = "in viverra metus facilisis sed", Image = "Assets/Financial4.png" });
        items.Add(new NewsItem { Id = 4, Category = "Financial", Headline = "Proin sem neque", Subhead = "aliqut quis ipsum tincidunt", Dateline = "Integer eleifend", Image = "Assets/Financial5.png" });
        items.Add(new NewsItem { Id = 5, Category = "Financial", Headline = "Mauris bibendum non leo vitae tempor", Subhead = "In nisl tortor, eleifend sed ipsum eget", Dateline = "Curabitur dictum augue vitae elementum ultrices", Image = "Assets/Food.png" });
        items.Add(new NewsItem { Id = 6, Category = "Food", Headline = "Lolem Ipsum", Subhead = "doro sit amet", Dateline = "Nunc tristique nec", Image = "Assets/Food1.png" });
        items.Add(new NewsItem { Id = 7, Category = "Food", Headline = "Etian ac felis viverra", Subhead = "vulputate nisl ac, aliquet nisi", Dateline = "tortor porttitor, eu fermentum ante congue", Image = "Assets/Food2.png" });
        items.Add(new NewsItem { Id = 8, Category = "Food", Headline = "Integer sed turpis erat", Subhead = "Sed quis hendrerit lorem, quis interdum dolor", Dateline = "in viverra metus facilisis sed", Image = "Assets/Food3.png" });
        items.Add(new NewsItem { Id = 9, Category = "Food", Headline = "Proin sem neque", Subhead = "aliqut quis ipsum tincidunt", Dateline = "Integer eleifend", Image = "Assets/Food4.png" });
        items.Add(new NewsItem { Id = 10, Category = "Food", Headline = "Mauris bibendum non leo vitae tempor", Subhead = "In nisl tortor, eleifend sed ipsum eget", Dateline = "Curabitur dictum augue vitae elementum ultrices", Image = "Assets/Food5.png" });
        return items;
    }

}

