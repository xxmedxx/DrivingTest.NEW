using System;
using System.Collections.Generic;

namespace App.Models
{
    public class ExamModel
    {
       public int Id { get; set; }
        public System.DateTime TestDate { get; set; }
        public int TestScore { get; set; }
        public int TotalTime { get; set; }
        public string UserId { get; set; }
    
        public virtual AppUserModel AspNetUser { get; set; }
        public virtual ICollection<TestLineModel> TestTries { get; set; }
    }
}
