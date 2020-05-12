using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using SpaceCafe.Adapter;

namespace SpaceCafe.Resources.Fragments
{
    public class ContactFragment : Fragment
    {
        ViewPager viewPager;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.contact, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // loads  image carousel
            viewPager = view.FindViewById<ViewPager>(Resource.Id.contactCarousel);

            int[] images =
            {
               Resource.Drawable.sausage_bap,
               Resource.Drawable.burger,
               Resource.Drawable.space_logo
            };

            CarouselAdapter carouselAdapter = new CarouselAdapter(view.Context, images);
            viewPager.OffscreenPageLimit = 2;
            viewPager.Adapter = carouselAdapter;

        }
    }
}