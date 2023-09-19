using System;

namespace TU_Challenge.Heritage
{

    public class Animal
    {
        private string _name;

        public string Name { get => _name; }

        public int Pattes { get; set; }

        public event Action OnDie;

        public Animal(string name)
        {
            _name = name;
        }

        public virtual void ArriveInPetShop(Animalerie master)
        {
            throw new NotImplementedException();

        }
        internal bool IsAlive()
        {
            throw new NotImplementedException();
        }
        internal void Die()
        {
            throw new NotImplementedException();
        }

        internal bool Crier()
        {
            throw new NotImplementedException();
        }
    }
}
