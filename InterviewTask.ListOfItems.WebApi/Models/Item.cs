using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.ListOfItems.WebApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}