using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using SpaceCafe.Models;
using Firebase.Firestore;

namespace SpaceCafe.Adapter
{
    class OrderAdapter : RecyclerView.Adapter
    {
        public event EventHandler<OrderAdapterClickEventArgs> ItemClick;
        public event EventHandler<OrderAdapterClickEventArgs> ItemLongClick;
        List<Order> items;
        Android.Content.Context context;
        FirebaseFirestore db;

        public OrderAdapter(List<Order> data, Android.Content.Context context)
        {
            items = data;
            this.context = context;
            db = FirebaseFirestore.GetInstance(FirestoreUtils.App);
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cart_row, parent, false);
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            var vh = new OrderAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as OrderAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.OrderItem.Text = $"{items[position].OrderedItem} x {items[position].Quantity}";
            holder.OrderPrice.Text = $"£{items[position].TotalCost.ToString("0:.00")}";

            holder.btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(context);
           // Android.App.AlertDialog confirm = dialog.Create();
            dialog.SetTitle("Remove Item");
            dialog.SetMessage("Are you sure you want to remove this item from your cart?");
            //dialog.SetPositiveButton("Remove");
        }

        public override int ItemCount => items.Count;

        void OnClick(OrderAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(OrderAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class OrderAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView OrderItem { get; set; }
        public TextView OrderPrice { get; set; }
        public ImageButton btnDelete { get; set; }

        public OrderAdapterViewHolder(View itemView, Action<OrderAdapterClickEventArgs> clickListener,
                            Action<OrderAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            OrderItem = itemView.FindViewById<TextView>(Resource.Id.txtItemOrdered);
            OrderPrice = itemView.FindViewById<TextView>(Resource.Id.txtCost);
            btnDelete = ItemView.FindViewById<ImageButton>(Resource.Id.delete);

            itemView.Click += (sender, e) => clickListener(new OrderAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new OrderAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class OrderAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}