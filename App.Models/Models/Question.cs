
using System;
using System.Collections.Generic;

namespace App.Models
{
    public class QuestionModel
    {
        public int ID { get; set; }
        public int QuestionNumber { get; set; }
        public string Picture { get; set; }
        public string Sound { get; set; }
        public int Choice1 { get; set; }
        public int Choice2 { get; set; }
        public Nullable<int> Choice3 { get; set; }
        public Nullable<int> Choice4 { get; set; }
        public string CorrectAnswer { get; set; }
        public int SeriesID { get; set; }


    }
}
