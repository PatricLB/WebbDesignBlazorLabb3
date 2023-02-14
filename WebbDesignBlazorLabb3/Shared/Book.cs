using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbDesignBlazorLabb3.Shared;

    public class Book
    {
        public int Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }

        public Book(int isbn, string title, string desc, string auth, int price)
        {
            this.Isbn = isbn;
            this.Title = title;
            this.Description = desc;
            this.Author = auth;
            this.Price = price;
        }

    }


