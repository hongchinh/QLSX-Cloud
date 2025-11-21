using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class NotificationModel
    {
        public int Count {get; set; }
        public JsonResult Item { get; set; }
        public int Id { get; set; }
    }
}
