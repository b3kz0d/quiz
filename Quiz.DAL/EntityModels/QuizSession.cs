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
    
    public partial class QuizSession
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuizSession()
        {
            this.Results = new HashSet<Result>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int QuizOptionId { get; set; }
        public System.DateTime SessionStart { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual QuizOption QuizOption { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}
