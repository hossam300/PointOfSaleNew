using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class PiChartsDTO
    {
        public string Text { get; set; }
        public double Value { get; set; }
    }
    public class ChartsDTO
    {
        public string CategoryName { get; set; }
        public List<SeriesDTO> Series { get; set; }
    }
    public class SeriesDTO
    {
        public string Text { get; set; }
        public double Value { get; set; }
    }
}
