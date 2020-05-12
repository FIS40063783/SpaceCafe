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
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;

namespace SpaceCafe
{

    class FirestoreUtils
    {
        public static FirebaseApp App { get; set; }
        public static FirebaseAuth Auth { get; set; }
        public static void GetDatabase(Android.Content.Context context)
        {
            var options = new FirebaseOptions.Builder()
            .SetProjectId("spacecafe-284f8")
            .SetApplicationId("spacecafe-284f8")
            .SetApiKey("AIzaSyCSS7zqkDXlLpvD5ezA85PsGTFMdnKT_BU")
            .SetDatabaseUrl("https://spacecafe-284f8.firebaseio.com")
            .SetStorageBucket("spacecafe-284f8.appspot.com")
            .Build();

            if (FirebaseApp.GetApps(context).Count < 1)
            {
                App = FirebaseApp.InitializeApp(context, options);
            }

            Auth = FirebaseAuth.Instance;
        }

        public static void CreateOrder()
        {
            FirebaseFirestore db = FirebaseFirestore.GetInstance(App);
            CollectionReference users = db.Collection("Users");
        }
    }
}
