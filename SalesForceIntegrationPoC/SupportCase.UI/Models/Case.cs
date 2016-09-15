using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportCase.UI.Models
{
    public class Case
    {
        public string txtCompanyName { get; set; }
        public string txtForename { get; set; }
        public string txtSurname { get; set; }
        public string txtTelephone { get; set; }
        public string txtEmail { get; set; }
        public int cboproduct { get; set; }
        public int txtVersion { get; set; }
        public string txtProblem { get; set; }
        public int cbowindows { get; set; }
        public int cbonetwork { get; set; }
    }
}