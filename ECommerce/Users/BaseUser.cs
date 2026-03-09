using System;
using ECommerce.Repositories;

namespace ECommerce.Users
{
    public abstract class BaseUser : IUser
    {
        protected readonly IProductRepository _repo;
        public string Name { get; }

        protected BaseUser(string name, IProductRepository repo)
        {
            Name = name;
            _repo = repo;
        }

        protected void ViewProducts()
        {
            Console.WriteLine("\n--- Products ---");
            foreach (var p in _repo.GetAll())
                Console.WriteLine(p);
        }

        public abstract void ShowMenu();
    }
}