using StackOverflow25.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow25.Web.Models
{
    public class ViewQuestionViewModel
    {
        public Question Question { get; set; }
        public bool CanLike { get; set; }
    }
}
