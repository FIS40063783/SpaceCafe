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

namespace SpaceCafe
{
    [Activity(Label = "ItemMenu")]
    public class ItemMenu : Activity
    {
        Button burger;
        Button sausageBap;
        Button sausageRoll;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.item_menu);
            burger = FindViewById<Button>(Resource.Id.burger_button);
            sausageBap = FindViewById<Button>(Resource.Id.sausage_bap_button);
            sausageRoll = FindViewById<Button>(Resource.Id.sausage_roll_button);

            burger.Click += Burger_Click;
            sausageBap.Click += SausageBap_Click;
            sausageRoll.Click += SausageRoll_Click;
        }

        private Intent PassItem(string btnText)
        {
            Intent intent = new Intent(this, typeof(OrderActivity));
            intent.PutExtra("Food_Item", btnText);
            return intent;
        }

        private void SausageRoll_Click(object sender, EventArgs e)
        {
            StartActivity(PassItem(sausageRoll.Text));
        }

        private void SausageBap_Click(object sender, EventArgs e)
        {
            StartActivity(PassItem(sausageBap.Text));
        }

        private void Burger_Click(object sender, EventArgs e)
        {
            StartActivity(PassItem(burger.Text));
        }
    }
}