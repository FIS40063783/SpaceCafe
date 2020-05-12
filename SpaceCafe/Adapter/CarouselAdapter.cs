using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace SpaceCafe.Adapter
{
    class CarouselAdapter : PagerAdapter
    {

        Context context;
        LayoutInflater inflater;
        int[] images;

        public CarouselAdapter(Context context, int[] images)
        {
            this.context = context;
            this.images = images;
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }


        public Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public  long GetItemId(int position)
        {
            return position;
        }

        public View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            CarouselAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as CarouselAdapterViewHolder;

            if (holder == null)
            {
                holder = new CarouselAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                //view = inflater.Inflate(Resource.Layout.item, parent, false);
                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                view.Tag = holder;
            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == ((LinearLayout)obj);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View itemView = inflater.Inflate(Resource.Layout.carousel, container, false);

            ImageView image = itemView.FindViewById<ImageView>(Resource.Id.carouselImage);
            image.SetImageResource(images[position]);

            container.AddView(itemView);

            return itemView;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
        {
            container.RemoveView((LinearLayout)obj);
        }


        //Fill in cound here, currently 0
        public override int Count => images.Length;

    }

    class CarouselAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}