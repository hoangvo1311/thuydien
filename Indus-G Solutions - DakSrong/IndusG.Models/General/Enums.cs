using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndusG.Models
{
    public class Enums
    {
        public enum CPUType
        {
            [Display(Name = "S7-200")]
            S7200,
            [Display(Name = "S7-300")]
            S7300,
            [Display(Name = "S7-400")]
            S7400,
            [Display(Name = "S7-1200")]
            S71200,
            [Display(Name = "S7-1500")]
            S71500
        }

        public enum UserType
        {
            Admin,
            Operator,
            ShiftLeader
        }
    }
}