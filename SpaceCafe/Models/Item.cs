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

using Firebase;
using Firebase.Provider;
using Firebase.Firestore;

namespace SpaceCafe.Models
{
    class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}