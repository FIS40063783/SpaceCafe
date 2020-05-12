using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Firestore;

namespace SpaceCafe
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button signIn;
        Button createAccount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            signIn = FindViewById<Button>(Resource.Id.sign_in_button);
            createAccount = FindViewById<Button>(Resource.Id.create_account_button);
            FirestoreUtils.GetDatabase(this);

            signIn.Click += LoadSignIn;
            createAccount.Click += LoadSignUp;
        }

        private void LoadSignIn(object sender, EventArgs e)
        {
            StartActivity(typeof(Login));
        }

        private void LoadSignUp(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUp));
        }
       
    }
}

