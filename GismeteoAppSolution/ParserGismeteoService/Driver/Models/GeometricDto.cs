using System;
using System.Collections.Generic;
using System.Text;

namespace ParserGismeteoService.Driver.Models
{
    public class GeometricDto
    {
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
     }
}
