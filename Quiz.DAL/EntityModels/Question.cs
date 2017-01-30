
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
    
public partial class Question
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Question()
    {

        this.QuestionAnswers = new HashSet<QuestionAnswer>();

        this.Results = new HashSet<Result>();

    }


    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int QuestionLevelId { get; set; }

    public string QuestionContent { get; set; }



    public virtual Category Category { get; set; }

    public virtual QuestionLevel QuestionLevel { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Result> Results { get; set; }

}

}
