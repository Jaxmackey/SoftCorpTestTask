using System;
using System.Collections.Generic;
using System.Text;

namespace ParserGismeteoService.Driver.Models
{
    public class CityGeometricDto
    {
        public Guid CityId { get; set; }
        public List<GeometricDto> Geometrics { get; set; }
        public RunDto Run { get; set; }
    }
}
