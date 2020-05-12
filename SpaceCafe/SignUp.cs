using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Lang;

namespace SpaceCafe
{
    [Activity(Label = "SignUp")]
    public class SignUp : Activity, IOnSuccessListener, IOnFailureListener
    {
        TextView signupError;
        EditText password;
        EditText confirmPassword;
        EditText email;
        FirebaseAuth auth = FirestoreUtils.Auth;

        public void OnFailure(Java.Lang.Exception e)
        {
            signupError.Text = e.Message;
            signupError.Visibility = ViewStates.Visible;
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            FirebaseUser user = auth.CurrentUser;
            StartActivity(typeof(MainMenu));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.sign_up);

            //set up form controls
            signupError = FindViewById<TextView>(Resource.Id.signup_error);
            Button signup = FindViewById<Button>(Resource.Id.create_account);
            Button sendToLogin = FindViewById<Button>(Resource.Id.send_to_login);
            password = FindViewById<EditText>(Resource.Id.password);
            confirmPassword = FindViewById<EditText>(Resource.Id.confirm_password);
            email = FindViewById<EditText>(Resource.Id.Email);

            signup.Click += CreateAccount;
            sendToLogin.Click += SendToLogin_Click;
        }

        private void SendToLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Login));
        }

        // validates details and creates account
        private void CreateAccount(object sender, EventArgs e)
        {
            if (password.Text != confirmPassword.Text)
            {
                signupError.Text = "Your passwords did not match";
                signupError.Visibility = ViewStates.Visible;
            }
            else if(IsEmpty(email) || IsEmpty(password))
            {
                signupError.Text = "All fields must be filled";
                signupError.Visibility = ViewStates.Visible;
            }
            else
            {
                auth.CreateUserWithEmailAndPassword(email.Text, password.Text).AddOnSuccessListener(this).AddOnFailureListener(this);
            }
            
        }

        private bool IsEmpty(EditText field)
        {
            return field.Text.Length == 0;
        }
    }
}