using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Firebase.Auth;
using SpaceCafe.Adapter;
using SpaceCafe.Models;
using Android.Gms.Tasks;
using Java.Lang;

namespace SpaceCafe
{
    [Activity(Label = "Cart")]
    public class Cart : Activity, IOnSuccessListener, IOnFailureListener
    {
        Button btnPurchase;
        ImageView delete;

        FirebaseFirestore db;
        FirebaseAuth auth;

        RecyclerView orderedList;
        List<Order> userOrders;

        Query orderFilter;
        QuerySnapshot orders;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.cart);

            btnPurchase = FindViewById<Button>(Resource.Id.btnPurchase);
            delete = FindViewById<ImageView>(Resource.Id.delete);

            orderedList = FindViewById<RecyclerView>(Resource.Id.rcvOrders);
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
                userOrders.Clear();
            }
        }

        private void InitialiseRecyclerView()
        {
            orderedList.SetLayoutManager(new LinearLayoutManager(orderedList.Context));
            OrderAdapter adapter = new OrderAdapter(userOrders, this);
            orderedList.SetAdapter(adapter);
        }

        private void FetchData()
        {
            Query findOrders = db.Collection("Orders").WhereEqualTo("Uid", auth.CurrentUser.Uid);
            findOrders.Get().AddOnSuccessListener(this).AddOnFailureListener(this);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            orders = (QuerySnapshot)result;

            if (!orders.IsEmpty)
            {
                foreach (DocumentSnapshot item in orders.Documents)
                {
                    userOrders.Add(new Order
                    {
                        OrderedItem = item.GetString("Item")
                    });
                }

                InitialiseRecyclerView();
            }
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(this, e.Message, ToastLength.Short).Show();
        }
    }
}