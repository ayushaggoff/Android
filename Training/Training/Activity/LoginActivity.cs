using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Org.Json;
using Training.Model;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace Training.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/LoginTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity, IFacebookCallback,GraphRequest.IGraphJSONObjectCallback
    {
        ICallbackManager mCallBackManager;
        MyProfileTracker mProfileTracker;
        ImageButton btn_fb;
        string Name;
        Button btnLogin;
        EditText email, password;
        CheckBox mCbxRemMe;

        Android.App.AlertDialog.Builder dialog;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            await TryToGetPermission();
            FacebookSdk.SdkInitialize(this.ApplicationContext);


            SetContentView(Resource.Layout.LoginLayout);

           


            btn_fb = FindViewById<ImageButton>(Resource.Id.imageButton1);
            mCallBackManager = CallbackManagerFactory.Create();


            LoginManager.Instance.RegisterCallback(mCallBackManager, this);
            btn_fb.Click += (o, e) =>
            {

                LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "public_profile", "user_friends","email" });

            };


            email = FindViewById<EditText>(Resource.Id.editText_email);
            password = FindViewById<EditText>(Resource.Id.editText_password);
            btnLogin = FindViewById<Button>(Resource.Id.buttonlogin);
            mCbxRemMe = FindViewById<CheckBox>(Resource.Id.cbxRememberMe);
            dialog = new Android.App.AlertDialog.Builder(this);

            //  btnLogin.Click += BtnLogin_Click;
        }

         protected override void OnResume()
        {
            base.OnResume();
            this.btnLogin.Click += this.BtnLogin_Click;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.btnLogin.Click -= this.BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DashboardActivity));

            if (CheckValidation(email.Text, password.Text))
            {
                User user = new User()
                {
                    Email = email.Text,
                    Password = password.Text
                };

                intent.PutExtra("User", JsonConvert.SerializeObject(user));

                if (mCbxRemMe.Checked)
                {
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Email", email.Text.Trim());
                    edit.PutString("Password", password.Text.Trim());
                    edit.Apply();
                }
                this.StartActivity(intent);
                this.Finish();
            }
        }
        public bool CheckValidation(string email, string password)
        {
            Android.App.AlertDialog alert = dialog.Create();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter All Fields");
                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.Show();
                return false;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter Valid Email");

                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.SetButton2("CANCEL", (c, ev) =>
                {

                });
                alert.Show();
                return false;
            }
            else
            {
                return true;
                //alert.SetTitle("Login");
                //alert.SetMessage("Success");

                //alert.SetButton("OK", (c, ev) =>
                //{
                //    Intent intent = new Intent(this, typeof(VerificationCodeActivity));
                //    StartActivity(intent);

                //   intent.PutExtra("email", userObj.Email);
                //    StartActivity(intent);
                //});
                //alert.SetButton2("CANCEL", (c, ev) =>
                //{

                //});
                //alert.Show();
            }
        }



        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            GraphRequest request = GraphRequest.NewMeRequest(AccessToken.CurrentAccessToken, this);
            Bundle parameters = new Bundle();
            parameters.PutString("fields", "id,name,age_range,email");
            request.Parameters = parameters;
            request.ExecuteAsync();

           
        }
        public void OnCompleted(JSONObject json, GraphResponse response)
        {
            string data = json.ToString();
            FacebookResult result = JsonConvert.DeserializeObject<FacebookResult>(data);
            Console.WriteLine(json.ToString());
            Name = result.email;

            Intent intent = new Intent(this, typeof(DashboardActivity));
            intent.PutExtra("Name", Name);


            this.StartActivity(intent);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }



        #region permisson
        async Task TryToGetPermission()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermission();
                return;
            }
        }

        const int RequestLocationId = 0;

        readonly string[] PermissionGroupLocation =
       {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        async Task GetPermission()
        {
            string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                Toast.MakeText(this, "Loaction Permission Granted", ToastLength.Short).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("The application need special permissions to continue");
                alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionGroupLocation, RequestLocationId);
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionGroupLocation, RequestLocationId);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();

                        }
                        else
                        {

                            Toast.MakeText(this, "Special permissions denied", ToastLength.Short).Show();

                        }
                    }
                    break;
            }

        }

        #endregion
    }
}