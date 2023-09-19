using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Animalerie
    {
        static Animalerie _instance;
        public static Animalerie Instance => _instance;

        List<Animal> _zoo;

        public List<Animal> Zoo { get => _zoo; }

        public event Action<Animal> OnAddAnimal;

        public Animalerie()
        {
            _zoo = new List<Animal>();
        }   

        public void AddAnimal(Animal c)
        {
           throw new NotImplementedException();
        }

        public bool Contains(Animal a)
        {
           throw new NotImplementedException();
        }

        public Animal GetAnimal(int index)
        {
           throw new NotImplementedException();
        }

        public void FeedAll()
        {
           throw new NotImplementedException();
        }
    }
}
