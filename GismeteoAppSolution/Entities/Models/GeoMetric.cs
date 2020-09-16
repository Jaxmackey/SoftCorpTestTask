using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public partial class GeoMetric
    {
        public Guid Id { get; set; }
        public string DayName { get; set; }
        public string DayNumber { get; set; }
        public string MaxTempC { get; set; }
        public string MaxTempF { get; set; }
        public string MinTempC { get; set; }
        public string MinTempF { get; set; }
        public string WindMs { get; set; }
        public string MiH { get; set; }
        public string KmH { get; set; }
        public string Prec { get; set; }
        public Guid FkRunId { get; set; }

        public virtual Run FkRun { get; set; }
    }
}
