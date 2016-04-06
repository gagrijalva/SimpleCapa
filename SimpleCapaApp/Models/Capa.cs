using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleCapaApp.Models
{
    public class Capa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Capa Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [DisplayName("Supervisor")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser Supervisor { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

    }

}