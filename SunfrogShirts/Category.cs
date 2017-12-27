using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunfrogShirts
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Category()
        {

        }

        public List<Category> getListCategory()
        {
            List<Category> ls = new List<Category>();
            ls.Add(new Category { Id = 8, Name = "Guys Tee", Price = 19 });
            ls.Add(new Category { Id = 34, Name = "Ladies Tee", Price = 19 });
            ls.Add(new Category { Id = 35, Name = "Youth Tee", Price = 19 });
            ls.Add(new Category { Id = 19, Name = "Hoodie", Price = 34 });
            ls.Add(new Category { Id = 27, Name = "Sweat Shirt", Price = 31 });
            ls.Add(new Category { Id = 50, Name = "Guys V-Neck", Price = 23 });
            ls.Add(new Category { Id = 116, Name = "Ladies V-Neck", Price = 23 });
            ls.Add(new Category { Id = 118, Name = "Unisex Tank Top", Price = 19 });
            ls.Add(new Category { Id = 119, Name = "Unisex Long Sleeve", Price = 25 });
            ls.Add(new Category { Id = 120, Name = "Leggings", Price = 14 });
            ls.Add(new Category { Id = 128, Name = "Coffee Mug (colored)", Price = 14 });
            ls.Add(new Category { Id = 129, Name = "Coffee Mug (white)", Price = 14 });
            ls.Add(new Category { Id = 145, Name = "Coffee Mug (color change)", Price = 14 });
            ls.Add(new Category { Id = 137, Name = "Posters 16x24", Price = 14 });
            ls.Add(new Category { Id = 138, Name = "Posters 24x16", Price = 14 });
            ls.Add(new Category { Id = 139, Name = "Posters 11x17", Price = 14 });
            ls.Add(new Category { Id = 140, Name = "Posters 17x11", Price = 14 });
            ls.Add(new Category { Id = 143, Name = "Canvas 16x20", Price = 14 });
            ls.Add(new Category { Id = 147, Name = "Hat", Price = 15 });
            ls.Add(new Category { Id = 148, Name = "Trucker Cap", Price = 18 });

            return ls;
        }

        public List<Category> getListCategoryProduct()
        {
            List<Category> ls = new List<Category>();

            ls.Add(new Category { Id = 52, Name = "Automotive" });
            ls.Add(new Category { Id = 76, Name = "Birth Years" });
            ls.Add(new Category { Id = 78, Name = "Drinking" });
            ls.Add(new Category { Id = 26, Name = "Faith" });
            ls.Add(new Category { Id = 61, Name = "Fitness" });
            ls.Add(new Category { Id = 19, Name = "Funny" });
            ls.Add(new Category { Id = 13, Name = "Gamer" });
            ls.Add(new Category { Id = 24, Name = "Geek-Tech" });
            ls.Add(new Category { Id = 82, Name = "Hobby" });
            ls.Add(new Category { Id = 35, Name = "Holidays" });
            ls.Add(new Category { Id = 79, Name = "Jobs" });
            ls.Add(new Category { Id = 43, Name = "LifeStyle" });
            ls.Add(new Category { Id = 12, Name = "Movies" });
            ls.Add(new Category { Id = 71, Name = "Music" });
            ls.Add(new Category { Id = 75, Name = "Names" });
            ls.Add(new Category { Id = 81, Name = "Outdoor" });
            ls.Add(new Category { Id = 62, Name = "Pets" });
            ls.Add(new Category { Id = 17, Name = "Political" });
            ls.Add(new Category { Id = 27, Name = "Sports" });
            ls.Add(new Category { Id = 77, Name = "States" });
            ls.Add(new Category { Id = 34, Name = "TV Shows" });
            ls.Add(new Category { Id = 11, Name = "Zombies" });

            return ls;
        }
    }
}
