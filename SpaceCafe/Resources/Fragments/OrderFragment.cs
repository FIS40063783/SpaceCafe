using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using SpaceCafe.Models;

namespace SpaceCafe.Resources.Fragments
{
    public class OrderFragment : Fragment, IOnSuccessListener
    {
        // Initialises control objects
        int orderQuantity;
        TextView quantity;
        TextView itemPrice;
        TextView itemInfo;

        LinearLayout rootView;

        FirebaseFirestore db;
        FirebaseAuth auth = FirestoreUtils.Auth;

        List<Item> products = new List<Item>();
        Item current;

        Button increaseQuantity;
        Button decreaseQuantity;
        Button addToCart;
        Spinner foodChoice;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.order_activity, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // Assigns the control objects to their XAML counterparts
            orderQuantity = 1;
            quantity = view.FindViewById<TextView>(Resource.Id.orderQuantity);
            itemInfo = view.FindViewById<TextView>(Resource.Id.description);
            itemPrice = view.FindViewById<TextView>(Resource.Id.price);
            foodChoice = view.FindViewById<Spinner>(Resource.Id.allItems);
            rootView = view.FindViewById<LinearLayout>(Resource.Id.rootView);
            increaseQuantity = view.FindViewById<Button>(Resource.Id.quantityIncrease);
            decreaseQuantity = view.FindViewById<Button>(Resource.Id.quantityDecrease);
            addToCart = view.FindViewById<Button>(Resource.Id.add_to_cart);
            db = FirebaseFirestore.GetInstance(FirestoreUtils.App);

            // Setting up event listeners
            foodChoice.ItemSelected += FoodChoice_ItemSelected;

            increaseQuantity.Click += IncreaseQuantity_Click;
            decreaseQuantity.Click += DecreaseQuantity_Click;
            addToCart.Click += AddToCart_Click;

            CollectionReference cr = db.Collection("Items");

            cr.Get().AddOnSuccessListener(this);
        }

        private void DecreaseQuantity_Click(object sender, EventArgs e)
        {
            if (orderQuantity > 1)
            {
                quantity.Text = (--orderQuantity).ToString();
            }
        }

        private void IncreaseQuantity_Click(object sender, EventArgs e)
        {
            if (orderQuantity < 5)
            {
                quantity.Text = (++orderQuantity).ToString();
            }
        }

        // Gets item information
        private void FoodChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            current = products.Find(x => x.Name.Equals(foodChoice.SelectedItem.ToString()));
            itemPrice.Text = current.Price.ToString("C");
            itemInfo.Text = current.Type;
        }

        // Stores order in datrtabase
        private void AddToCart_Click(object sender, EventArgs e)
        {
            DocumentReference docref = db.Collection("Orders").Document();
            Dictionary<string, Java.Lang.Object> order = new Dictionary<string, Java.Lang.Object>
            {
                {"Item", current.Name},
                {"Price", (current.Price * (decimal)orderQuantity).ToString()},
                {"Quantity", quantity.Text },
                {"Uid", auth.CurrentUser.Uid}
            };
            docref.Set(order);

            Snackbar.Make(rootView, "Added to cart!", Snackbar.LengthShort).Show();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            QuerySnapshot snap = (QuerySnapshot)result;
            List<string> itemNames = new List<string>();

            foreach (var res in snap.Documents)
            {
                itemNames.Add(res.GetString("ItemName"));
                products.Add(new Item
                {
                    Name = res.GetString("ItemName"),
                    Type = res.GetString("Information"),
                    Price = Convert.ToDecimal(res.GetDouble("Price"))
                });
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, itemNames);

            foodChoice.Adapter = adapter;
        }
    }
}