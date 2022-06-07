using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace WebApiEF_webshop.Models
{
    public partial class DTO_Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Price { get; set; }
        public string Imglink { get; set; }

        public DTO_Product(int id, string name, string description, int price, string imglink)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Imglink = imglink;

        }
    }
}
