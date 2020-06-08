using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;

namespace Training.Activity
{
    [Activity(Label = "GoogleActivity" ,MainLauncher =false)]
    public class GoogleActivity : AppCompatActivity, IOnSuccessListener, IOnFailureListener

    {

        Button signinButton;
        TextView displayNameText;
        TextView emailText;
        TextView photourlText;
        GoogleSignInOptions gso;
        GoogleApiClient googleApiClient;



        FirebaseAuth firebaseAuth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GoogleLoginFireBaseLayout);



            signinButton = (Button)FindViewById(Resource.Id.sign_in_button);
            displayNameText = (TextView)FindViewById(Resource.Id.textView1);
            emailText = (TextView)FindViewById(Resource.Id.emailText);
            signinButton.Click += SigninButton_Click;

            gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("823954020212-pous4rh94fc1i9rm1i3g46fi2j4ot4q1.apps.googleusercontent.com")
                .RequestEmail()
                .Build();

            googleApiClient = new GoogleApiClient.Builder(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso).Build();
            googleApiClient.Connect();

            firebaseAuth = GetFirebaseAuth();
            UpdateUI();

        }


        private FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(this);
            FirebaseAuth mAuth;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("training-ea64f")
                    .SetApplicationId("training-ea64f")
                    .SetApiKey("AIzaSyAYuOLei2bceIwSMgSVf5boMJPMnWxOS4c")
                    .SetDatabaseUrl("https://training-ea64f.firebaseio.com")
                    .SetStorageBucket("training-ea64f.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(this, options);

                mAuth = FirebaseAuth.Instance;

            }

            else

            {

                mAuth = FirebaseAuth.Instance;

            }

            return mAuth;

        }



        private void SigninButton_Click(object sender, System.EventArgs e)

        {

            UpdateUI();

            if (firebaseAuth.CurrentUser == null)

            {

                var intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);

                StartActivityForResult(intent, 1);

            }

            else

            {

                firebaseAuth.SignOut();

                UpdateUI();

            }



        }



        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)

        {

            base.OnActivityResult(requestCode, resultCode, data);



            if (requestCode == 1)

            {

                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);

                if (result.IsSuccess)

                {

                    GoogleSignInAccount account = result.SignInAccount;

                    LoginWithFirebase(account);

                }

            }

        }



        private void LoginWithFirebase(GoogleSignInAccount account)
        {

            var credentials = GoogleAuthProvider.GetCredential(account.IdToken, null);

            firebaseAuth.SignInWithCredential(credentials).AddOnSuccessListener(this)

                .AddOnFailureListener(this);

        }



        public void OnSuccess(Java.Lang.Object result)

        {

            displayNameText.Text = "Display Name: " + firebaseAuth.CurrentUser.DisplayName;

            emailText.Text = "Email: " + firebaseAuth.CurrentUser.Email;

            //photourlText.Text = "Photo URL: " + firebaseAuth.CurrentUser.PhotoUrl.Path;



            Toast.MakeText(this, "Login successful", ToastLength.Short).Show();

            UpdateUI();

        }



        public void OnFailure(Java.Lang.Exception e)

        {

            Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();

            UpdateUI();

        }



        void UpdateUI()

        {

            if (firebaseAuth.CurrentUser != null)

            {

                signinButton.Text = "Sign Out";

            }

            else

            {

                signinButton.Text = "Sign In With Google";

            }

        }

    }
}
