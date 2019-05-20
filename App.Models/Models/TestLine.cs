
using System;

namespace App.Models
{
    public partial class TestLineModel
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string SelectedAnswer { get; set; }
        public int QuestionId { get; set; }
        public Nullable<bool> AnswerStatut { get; set; }
    }
}
