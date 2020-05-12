using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SpaceCafe.Models
{
    class Order
    {
        public string OrderedItem { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
    }
}