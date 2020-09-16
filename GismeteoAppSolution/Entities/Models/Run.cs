using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Run
    {
        public Run()
        {
            GeoMetric = new HashSet<GeoMetric>();
        }

        public Guid Id { get; set; }
        public Guid FkCity { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public virtual City FkCityNavigation { get; set; }
        public virtual ICollection<GeoMetric> GeoMetric { get; set; }
    }
}
