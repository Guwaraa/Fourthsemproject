//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodOrderingSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Foodmenu
    {
        public int SN { get; set; }
        public string Foodname { get; set; }
        public string FoodDetail { get; set; }
        public Nullable<int> FoodPrice { get; set; }
        public string FoodCategory { get; set; }
        public string Foodpicture { get; set; }
    }
}