using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleCapaApp.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        public byte[] fileBytes { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

    }
}