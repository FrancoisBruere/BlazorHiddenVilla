using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Model.ViewModel
{
    public class HomeVM
    {

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EnddDate { get; set; }

        public int NoOfNights { get; set; }


    }
}
