using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTurn.Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string TelSms { get; set; }
        public bool TelConfirmed { get; set; }
        public string Email { get; set; }
    }
}