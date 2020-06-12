using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Android.Support.V4.App;
using Training.Activity;
using System.Runtime.Remoting.Contexts;

namespace Training
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        [Obsolete]
        public override void OnMessageReceived(RemoteMessage message)
        {

            //var li = ActivityManager.RunningTaskInfo.Creator.

            //  ActivityManager.RunningTaskInfo li = ActivityManager.RunningTaskInfo.NumActivities;

            ActivityManager mngr = (ActivityManager)GetSystemService(ActivityService);

            IList<ActivityManager.RunningTaskInfo> taskList = mngr.GetRunningTasks(10);
            int z = 0,i=0;
            foreach (ActivityManager.RunningTaskInfo task in taskList)
            {
                z++;
                if ("crc645aea788ebc5f01ab.DashboardActivity".Equals(task.BaseActivity.ClassName))
                {
                    i = 1;
                    break;
                }

            }
            //if (z > 0 && i == 1)
            //{
            //    Finish();
            
            //}
                Log.Debug("/////////////", "/////////////");
                foreach (var li in message.Data.Values)
                {
                    Log.Debug("/////////////////", li);

                }
                //   Log.Debug("****",message.Data.Values.ToString());  
                Log.Debug("//", "//");
                Log.Debug(TAG, "From: " + message.From);
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                var body = message.GetNotification().Body;
                SendNotification(body, message.Data);
            }
            void SendNotification(string messageBody, IDictionary<string, string> data)
            {
                var intent = new Intent(Application.Context, typeof(DashboardActivity));

                intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra("notification", "You have clciked on Notification");
                

                var pendingIntent = PendingIntent.GetActivity(Application.Context,
                                                              DashboardActivity.NOTIFICATION_ID,
                                                              intent,
                                                              PendingIntentFlags.OneShot);

                var notificationBuilder = new NotificationCompat.Builder(Application.Context, DashboardActivity.CHANNEL_ID)
                                          .SetSmallIcon(Resource.Drawable.icon_notification)
                                          .SetContentTitle("FCM Message")
                                          .SetContentText(messageBody)
                                          .SetAutoCancel(true)
                                          .SetContentIntent(pendingIntent);

                var notificationManager = NotificationManagerCompat.From(Application.Context);
                notificationManager.Notify(DashboardActivity.NOTIFICATION_ID, notificationBuilder.Build());
            }



        }
    }