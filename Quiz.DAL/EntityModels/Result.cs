//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quiz.DAL.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Result
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Result()
        {
            this.AnswerResults = new HashSet<AnswerResult>();
        }
    
        public int Id { get; set; }
        public int QuizSessionId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionOrder { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public System.DateTime UpdatedTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerResult> AnswerResults { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuizSession QuizSession { get; set; }
    }
}
