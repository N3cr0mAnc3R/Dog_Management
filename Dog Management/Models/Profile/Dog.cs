using Dog_Management.Models.Common;
using System;

namespace Dog_Management.Models.Profile
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsMale { get; set; }
        public IndexType Color
        {
            get
            {
                if (this.color == null)
                {
                    color = new IndexType() { Id = ColorId, Name = ColorName };
                }
                return color;
            }
            set
            {
                if (this.color == null)
                {
                    Color = new IndexType() { Id = ColorId, Name = ColorName };
                }
                color = Color;
            }
        }
        public IndexType Breed
        {
            get
            {
                if (this.breed == null)
                {
                    breed = new IndexType() { Id = BreedId, Name = BreedName };
                }
                return breed;
            }
            set
            {
                if (this.breed == null)
                {
                    Breed = new IndexType() { Id = BreedId, Name = BreedName };
                }
                breed = Breed;
            }
        }
        public DateTime DateOfBirth { get; set; }
        public Dog Mother
        {
            get
            {
                if (this.mother == null && MotherId != 0)
                {
                    mother = new Dog() { Id = MotherId };
                }
                return mother;
            }
            set
            {
                if (this.mother == null && MotherId != 0)
                {
                    Mother = new Dog() { Id = MotherId };
                }
                mother = Mother;
            }
        }
        public Dog Father
        {
            get
            {
                if (this.father == null && FatherId != 0)
                {
                    father = new Dog() { Id = FatherId};
                }
                return father;
            }
            set
            {
                if (this.father == null && FatherId != 0)
                {
                    Father = new Dog() { Id = FatherId };
                }
                father = Father;
            }
        }
        public string BloodLine { get; set; }

        #region Скрытые поля для маппинга
        private IndexType color;
        private int ColorId { get; set; }
        private string ColorName { get; set; }
        private IndexType breed;
        private int BreedId { get; set; }
        private string BreedName { get; set; }
        private Dog mother;
        private int MotherId { get; set; }
        private Dog father;
        private int FatherId { get; set; }
        #endregion
    }
}