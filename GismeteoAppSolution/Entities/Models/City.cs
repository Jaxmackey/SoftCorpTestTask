using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class City
    {
        public City()
        {
            Run = new HashSet<Run>();
        }

        public Guid Id { get; set; }
        public string CityName { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Run> Run { get; set; }
    }
}
