//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.DataBaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestTry
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string SelectedAnswer { get; set; }
        public int QuestionId { get; set; }
        public Nullable<bool> AnswerStatut { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual Test Test { get; set; }
    }
}
