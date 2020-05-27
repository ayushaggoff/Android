using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Training.Activity
{
    [Activity(Label = "GalleryActivity1", MainLauncher = false)]
    public class GalleryActivity1 : AppCompatActivity
    {
        Button button;
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource   
            SetContentView(Resource.Layout.GalleryLayout);
            TryToGetPermission();
            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += ButtonOnClick;
        }

        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
        //    Bitmap bitmap = (Bitmap)data.Extras.Get("data");
        //    _imageView.SetImageBitmap(bitmap);
        //}
        //private void ButtonOnClick(object sender, System.EventArgs e)
        //{
        //    Intent intent = new Intent(MediaStore.ActionImageCapture);
        //    StartActivityForResult(intent, 0);
        //}


        private void ButtonOnClick(object sender, System.EventArgs e)
        {
            PopupMenu menu = new PopupMenu(this, button);
            menu.Inflate(Resource.Menu.mainMenu);
            menu.Show();

            menu.MenuItemClick += (s1, arg1) =>
            {
                Intent = new Intent();
                   Intent.SetType("image/*");
                    Intent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
            };

            menu.MenuItemClick += (s2, arg1) =>
            {

                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);

            };
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0 && (data != null))
            {
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                _imageView.SetImageBitmap(bitmap);
            }
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                _imageView.SetImageURI(uri);
            }
        }



        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
        //    Bitmap bitmap = (Bitmap)data.Extras.Get("data");
        //    _imageView.SetImageBitmap(bitmap);
        //}





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
            Manifest.Permission.Camera

        };

        async Task GetPermission()
        {
            string permission = Manifest.Permission.Camera;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                Toast.MakeText(this, "Camera Permission Granted", ToastLength.Short).Show();
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




        // Create a Method ButtonOnClick.   
        //private void ButtonOnClick(object sender, EventArgs eventArgs)
        //{
        //    Intent = new Intent();
        //    Intent.SetType("image/*");
        //    Intent.SetAction(Intent.ActionGetContent);
        //    StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        //}
        //// Create a Method OnActivityResult(it is select the image controller)   
        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
        //    {
        //        Android.Net.Uri uri = data.Data;
        //        _imageView.SetImageURI(uri);
        //    }
        //}
    }
}



