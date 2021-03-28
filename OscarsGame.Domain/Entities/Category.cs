using System;
using System.Collections.Generic;

namespace OscarsGame.Domain.Entities
{
    [Serializable]
    public class Category
    {
        public Category()
        {
            this.Nominations = new List<Nomination>();
        }

        public int Id { get; set; }
        public string CategoryTtle { get; set; }
        public string CategoryDescription { get; set; }
        public virtual ICollection<Nomination> Nominations { get; set; }
    }
}
