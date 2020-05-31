using System;
using System.Collections.Generic;

namespace ProyectoAPI.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string DateTime { get; set; }
        public string Text { get; set; }
        public int NewsId { get; set; }

        public virtual News News { get; set; }
    }
}
