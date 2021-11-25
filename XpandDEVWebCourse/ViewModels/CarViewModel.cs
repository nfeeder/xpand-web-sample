using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Models;

namespace XpandDEVWebCourse.Web.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int ExternalId { get; set; }
        public int NrBolts { get; set; }
    }
}
