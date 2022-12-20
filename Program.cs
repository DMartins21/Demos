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

            var r1 = products.Where(p => p.Category.Tier == 2);
            Print("Todos no Tier 2:", r1);
            Console.WriteLine("\n");

            var r2 = products.Where(p => p.Category.Tier == 1 && p.Price == 1100.0M);
            Print("Tier 1 e Preco igual a 1100:", r2);
            Console.WriteLine("\n");

            var r3 = products.Where(p => p.Category.Name == "Tools");
            Print("Todos na categoria Tools:", r3);
            Console.WriteLine("\n");

            var r4 = products.Where(p => p.Category.Name == "Eletronics");
            Print("Todos na categoria Eletronics:", r4);
            Console.WriteLine("\n");

            var r5 = products.Where(p => p.Category.Name == "Tools").Union(products.Where
                (p => p.Category.Name == "Eletronics"));
            Print("Todos na Categoria Eletronics e Tools:", r5);
            Console.WriteLine("\n");
            var r6 = products.Where(p => p.Price >= 1000.0M && p.Price <= 1500.0M);
            Print("Preco entre R$1000 e R$1500", r6);
            Console.WriteLine("\n");
            var r7 = products.Where(p => p.Name[0] == 'C').Select(p => new {
                p.Name,
                p.Price,
                CategoryName = p.Category.Name
            });
            Print("Nomes começados com 'C", r7);
            Console.WriteLine("\n");

            var r8 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("Tier 1 ordenado por preço e nome", r8);
            Console.WriteLine("\n");

            var r9 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("First or Default: \n" + r9 + "\n");

            var r10 = products.Where(p => p.Id == 1).First();
            Console.WriteLine("First:\n" + r10 + "\n");

            var r11 = products.Max(p => p.Price);
            var r12 = products.Min(p => p.Price);
            Console.WriteLine("Preço minimo e Maximo:" + r12 + ' ' + r11);

            var r13 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Soma dos preços da categoria 1:" + r13 + "\n");

            var r14 = products.Where(p => p.Category.Id == 3).Average(p => p.Price);
            Console.WriteLine("Média dos preços da categoria 3:" + r14 + "\n");

            var r15 = products.GroupBy(p => p.Category);
            foreach (IGrouping<Category, Product> group in r15)
            {
                Console.WriteLine($"Category: {group.Key.Name}:");
                foreach (Product p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
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

            public override string ToString()
            {
                return "ID | Name | Price | Category_Name | Tier" + "\n"
                    + Id + " | " + Name + " | " + Price.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")) + " | "
                    + Category.Name + " | " + Category.Tier;
            }
        }
    }
}

    public class Category
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Tier { get; set; }
    }
