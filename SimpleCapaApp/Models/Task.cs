using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleCapaApp.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Task Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Techitian")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser Technitian { get; set; }

        [Required]
        public int CapaId { get; set; }

        [ForeignKey("CapaId")]
        public virtual Capa Capa { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int CapaStep { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        public DateTime DueDate {get; set;}

        public virtual ICollection<File> Files { get; set; }

    }

    public enum Status
    {
        New,
        Pending,
        Completed
    }

    /*public enum Step
    {
        Identification,
        ContaintmentTasks,
        Containtment,
        CorrectionTasks,
        Correction,
        Verification,
        Finished
    }*/
}