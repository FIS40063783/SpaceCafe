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
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using SpaceCafe.Adapter;
using SpaceCafe.Models;

namespace SpaceCafe.Resources.Fragments
{
    public class CartFragment : Fragment, IOnSuccessListener, IOnFailureListener
    {
        decimal allItemsPrice;
        TextView total;

        LinearLayout cart;
        Button btnPurchase;
        ImageView delete;

        FirebaseFirestore db;
        FirebaseAuth auth;

        RecyclerView orderedList;
        List<Order> userOrders;

        Query orderFilter;
        QuerySnapshot orders;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.cart, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // Assigns values to the form controls

            allItemsPrice = 0.0M;

            total = view.FindViewById<TextView>(Resource.Id.totalCost);

            cart = view.FindViewById<LinearLayout>(Resource.Id.shopping_cart);

            btnPurchase = view.FindViewById<Button>(Resource.Id.btnPurchase);
            delete = view.FindViewById<ImageView>(Resource.Id.delete);

            orderedList = view.FindViewById<RecyclerView>(Resource.Id.rcvOrders);
            db = FirebaseFirestore.GetInstance(FirestoreUtils.App);
            auth = FirestoreUtils.Auth;

            btnPurchase.Click += BtnPurchase_Click;

            userOrders = new List<Order>();

            orderFilter = db.Collection("Orders").WhereEqualTo("Uid", auth.CurrentUser.Uid);

            FetchData();

           
        }

        private void BtnPurchase_Click(object sender, EventArgs e)
        {
            //orderFilter.Get().AddOnCompleteFilter(this);
            foreach (DocumentSnapshot record in orders.Documents)
            {
                record.Reference.Delete();
            }
            userOrders.Clear();
            Snackbar.Make(cart, "Order Successful", Snackbar.LengthShort).Show();
            StartFragment(new HomeFragment());
        }

        // loads database info
        private void InitialiseRecyclerView()
        {
            orderedList.SetLayoutManager(new LinearLayoutManager(orderedList.Context));
            OrderAdapter adapter = new OrderAdapter(userOrders, Activity);
            orderedList.SetAdapter(adapter);
        }

        // queeries database for orders
        private void FetchData()
        {
            Query findOrders = db.Collection("Orders").WhereEqualTo("Uid", auth.CurrentUser.Uid);
            findOrders.Get().AddOnSuccessListener(this).AddOnFailureListener(this);
        }

        // Adds all orders in the database to a list
        public void OnSuccess(Java.Lang.Object result)
        {
            orders = (QuerySnapshot)result;

            if (!orders.IsEmpty)
            {
                foreach (DocumentSnapshot item in orders.Documents)
                {
                    Order order = new Order
                    {
                        OrderedItem = item.GetString("Item"),
                        Quantity = Convert.ToInt32(item.GetString("Quantity")),
                        TotalCost = Convert.ToDecimal(item.GetString("Price"))
                    };

                    allItemsPrice += order.TotalCost;

                    userOrders.Add(order);
                }

                total.Text = $"£{allItemsPrice.ToString("0:.00")}";

                InitialiseRecyclerView();
            }
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(Activity, e.Message, ToastLength.Short).Show();
        }

        private void StartFragment(Fragment fragment)
        {
            FragmentManager.BeginTransaction().Replace(Resource.Id.menu_container, fragment).Commit();
        }
    }
}