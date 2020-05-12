using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SpaceCafe.Resources.Fragments
{
    public class HomeFragment : Fragment
    {
        Button viewItems;
        Button cart;
        Button help;
        Button about;
        Button contact;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.home_fragment, container, false);
        }

        // sets up main menu buttons
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            viewItems = view.FindViewById<Button>(Resource.Id.view_items);
            cart = view.FindViewById<Button>(Resource.Id.cart_button);
            help = view.FindViewById<Button>(Resource.Id.help_button);
            about = view.FindViewById<Button>(Resource.Id.about_button);
            contact = view.FindViewById<Button>(Resource.Id.contact_button);

            viewItems.Click += ViewItems_Click;
            cart.Click += Cart_Click;
            help.Click += Help_Click;
            about.Click += About_Click;
            contact.Click += Contact_Click;
        }

        private void Contact_Click(object sender, EventArgs e)
        {
            StartFragment(new ContactFragment());
        }

        private void About_Click(object sender, EventArgs e)
        {
            StartFragment(new AboutFragment());
        }

        private void Help_Click(object sender, EventArgs e)
        {
            StartFragment(new HelpFragment());
        }

        private void Cart_Click(object sender, EventArgs e)
        {
            StartFragment(new CartFragment());
        }

        private void ViewItems_Click(object sender, EventArgs e)
        {
            StartFragment(new OrderFragment());
        }

        private void StartFragment(Fragment fragment)
        {
            FragmentManager.BeginTransaction().Replace(Resource.Id.menu_container, fragment).Commit();
        }
    }
}