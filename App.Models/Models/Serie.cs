
using System;
using System.Collections.Generic;

namespace App.Models
{
   public class SeriesModel
    {
        public int Id { get; set; }
        public int SeriesNumber { get; set; }
        public string Picture { get; set; }
        public Nullable<int> TotalQuestions { get; set; }

    }
}
