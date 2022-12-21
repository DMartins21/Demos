using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace Exercicios
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0M, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0M, Category = c1},
                new Product() { Id = 3, Name = "TV", Price = 1700.0M, Category = c3},
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0M, Category = c2},
                new Product() { Id = 5, Name = "Saw", Price = 80.0M, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 280.0M, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 90.0M, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 80.0M, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0M, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 180.0M, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0M, Category = c1 }
            };

            var r1 = from p in products
                     where p.Category.Tier == 1 && p.Price < 900.0M
                     select p;

            var r2 = from p in products where p.Category.Name == "Tools" select p.Name;

            var r3 = from p in products where p.Name[0] == 'C' 
                     select new { p.Name, p.Price, Category_Name = p.Category.Name };

            var r4 = from p in products
                     where p.Category.Tier == 1
                     orderby p.Name
                     orderby p.Price
                     select p;

            var r5 = (from p in r4 select p).Skip(2).Take(4);

            var r6 = from p in products group p by p.Category;

            Print("Tier 1 e Preço menor que 900", r1);
            Console.WriteLine();
            Print("Tools:", r2);
            Console.WriteLine();
            Print("Nomes começados com 'C'", r3);
            Console.WriteLine();
            Print("Ordenar por preço e por nome", r4);
            Console.WriteLine();
            Print("Tier 1 Order By Price then by skip 2 take 4", r5);
            Console.WriteLine();
            foreach(IGrouping<Category, Product> group in r6)
            {
                Console.WriteLine($"Category {group.Key.Name}");
                foreach(Product p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            Console.WriteLine("ID|Name|Price|Category_Name|Tier");
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }

    
        public class Product
        {
            public required int Id { get; set; }
            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public Category Category { get; set; }

            public override string ToString() => $"{Id}|{Name}|{Price.ToString("C",CultureInfo.CreateSpecificCulture("pt-BR"))}|{Category.Name}|{Category.Tier}";
        }
    }
}

    public class Category
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Tier { get; set; }
    }
