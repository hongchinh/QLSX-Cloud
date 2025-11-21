
using System;
using System.ComponentModel.DataAnnotations;

namespace Sale.Web.Data
{
    public class BaseSearchRequest
    {
        public int? UserId { get; set; }

       
        public DateTime DateNow { get; set; } = DateTime.Now;

         
        public DateTime? From { get; set; } = DateTime.Now;

        
        public DateTime? To { get; set; } = DateTime.Now.AddDays(10);

    }
}
