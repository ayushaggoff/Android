using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Training.Activity
{
    [Activity(Label = "ContactListActivity", MainLauncher = false)]
    public class ContactListActivity : ListActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            await TryToGetPermission();
            //  SetContentView(Resource.Layout.ContactListLayout);
            Try();
        }
        // Create your application here

        void Try()
        {



            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            string[] projection = {
            ContactsContract.Contacts.InterfaceConsts.Id,
             ContactsContract.CommonDataKinds.Phone.Number,
             ContactsContract.CommonDataKinds.Phone.InterfaceConsts.DisplayName

            //ContactsContract.Contacts.InterfaceConsts.DisplayName
        };
             var cursor = ManagedQuery(uri, projection, null, null, null);
            var contactList = new List<string>();
            if (cursor.MoveToFirst())
            {
                do
                {
                    contactList.Add(cursor.GetString(cursor.GetColumnIndex(projection[1])));
                     contactList.Add(cursor.GetString(cursor.GetColumnIndex(projection[2])));
                } while (cursor.MoveToNext());
            }
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ContactListLayout,Resource.Id.textView1, contactList);
        }



        async Task TryToGetPermission()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermission();
                return;
            }
        }

        const int RequestContactId = 0;

        readonly string[] PermissionGroupLocation =
       {
            Manifest.Permission.ReadContacts,
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
                    RequestPermissions(PermissionGroupLocation, RequestContactId);
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionGroupLocation, RequestContactId);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestContactId:
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
    }

}