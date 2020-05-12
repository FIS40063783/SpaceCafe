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
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Lang;

namespace SpaceCafe
{
    [Activity(Label = "Login")]
    public class Login : Activity, IOnSuccessListener, IOnFailureListener
    {
        EditText username;
        EditText password;

        Button login;
        Button sendToSignUp;

        TextView errorText;

        LinearLayout rootView;

        FirebaseFirestore db;
        FirebaseAuth auth = FirestoreUtils.Auth;

        public void OnFailure(Java.Lang.Exception e)
        {
            errorText.Text= $"Login Failed: {e.Message}";
            errorText.Visibility = ViewStates.Visible;
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            StartActivity(typeof(MainMenu));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // Load view
            SetContentView(Resource.Layout.login);

            // set up form controls
            username = FindViewById<EditText>(Resource.Id.login_username);
            password = FindViewById<EditText>(Resource.Id.login_password);
            login = FindViewById<Button>(Resource.Id.login);
            sendToSignUp = FindViewById<Button>(Resource.Id.btnCreateAccountPage);
            errorText = FindViewById<TextView>(Resource.Id.validation_error);
            rootView = FindViewById<LinearLayout>(Resource.Id.rootView);
            db = FirebaseFirestore.GetInstance(FirestoreUtils.App);

            login.Click += LoginUser;
            sendToSignUp.Click += SendToSignUp_Click;
        }

        // open sign up form
        private void SendToSignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUp));
        }

        // login user if all fields are filled
        private void LoginUser(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Text))
            {
               
                auth.SignInWithEmailAndPassword(username.Text, password.Text).AddOnSuccessListener(this).AddOnFailureListener(this);
            }
            else
            {
                errorText.Text = "All fields must be filled";
                errorText.Visibility = ViewStates.Visible;
            }

        }   
    }
}