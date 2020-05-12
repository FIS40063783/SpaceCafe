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
using SpaceCafe.Adapter;

namespace SpaceCafe
{
    [Activity(Label = "About")]
    public class About : Activity
    {
        ViewPager viewPager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.about);

            viewPager = FindViewById<ViewPager>(Resource.Id.imageCarousel);

            int[] images =
            {
                Resource.Drawable.burger,
                Resource.Drawable.sausage_bap,
                Resource.Drawable.space_logo
            };

            CarouselAdapter carouselAdapter = new CarouselAdapter(this, images);
            viewPager.OffscreenPageLimit = 2;
            viewPager.Adapter = carouselAdapter;
        }
    }
}