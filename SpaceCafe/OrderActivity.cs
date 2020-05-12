using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Firebase.Auth;
using Java.Lang;

using SpaceCafe.Models;

namespace SpaceCafe
{
    [Activity(Label = "OrderActivity")]
    public class OrderActivity : Activity, IOnSuccessListener
    {
        TextView foodItemName;
        TextView itemPrice;
        TextView itemInfo;

        LinearLayout rootView;

        FirebaseFirestore db;
        FirebaseAuth auth = FirestoreUtils.Auth;

        List<Item> products = new List<Item>();
        Item current;

        Button addToCart;
        Spinner foodChoice;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.order_activity);
            foodItemName = FindViewById<TextView>(Resource.Id.food_title);
            foodItemName.Text = Intent.GetStringExtra("Food_Item");
            itemInfo = FindViewById<TextView>(Resource.Id.description);
            itemPrice = FindViewById<TextView>(Resource.Id.price);
            foodChoice = FindViewById<Spinner>(Resource.Id.allItems);
            rootView = FindViewById<LinearLayout>(Resource.Id.rootView);
            addToCart = FindViewById<Button>(Resource.Id.add_to_cart);
            db = FirebaseFirestore.GetInstance(FirestoreUtils.App);

            foodChoice.ItemSelected += FoodChoice_ItemSelected;
            addToCart.Click += AddToCart_Click;

            CollectionReference cr = db.Collection("Items");

            cr.Get().AddOnSuccessListener(this);

           
        }

        private void FoodChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            current = products.Find(x => x.Name.Equals(foodChoice.SelectedItem.ToString()));
            itemPrice.Text = current.Price.ToString("C");
            itemInfo.Text = current.Type;
        }

        private void AddToCart_Click(object sender, EventArgs e)
        {
            DocumentReference docref = db.Collection("Orders").Document();
            Dictionary<string, Java.Lang.Object> order = new Dictionary<string, Java.Lang.Object>
            {
                {"Item", current.Name},
                {"Price", current.Price.ToString()},
                {"Uid", auth.CurrentUser.Uid}
            };
            docref.Set(order);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            QuerySnapshot snap = (QuerySnapshot)result;
            List<string> itemNames = new List<string>();
            
            foreach (var res in snap.Documents)
            {
                itemNames.Add(res.GetString("ItemName"));
                products.Add(new Item {
                    Name = res.GetString("ItemName"),
                    Type = res.GetString("Information"),
                    Price = Convert.ToDecimal(res.GetDouble("Price"))
                });
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, itemNames);

            foodChoice.Adapter = adapter;
        }
    }
}