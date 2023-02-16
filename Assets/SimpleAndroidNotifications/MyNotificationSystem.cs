using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
    public class MyNotificationSystem : MonoBehaviour
    {


        string GameName = "TowerArcher";
        public string message = "Time To Play!";
        private void Start()
        {

            GameName = Application.productName;
            DontDestroyOnLoad(this);

            SendNotification();
        }
        void SendNotification()
        {
#if !UNITY_EDITOR


            NotificationManager.CancelAll();
            ScheduleCustomRepeat(1, GameName, message, 3600f * (8f * 1f));
            ScheduleCustomRepeat(2, GameName, message, 3600f * (8f * 2f));
            ScheduleCustomRepeat(3, GameName, message, 3600f * (8f * 3f));
            ScheduleCustomRepeat(4, GameName, message, 3600f * (8f * 4f));
            ScheduleCustomRepeat(5, GameName, message, 3600f * (8f * 5f));
            ScheduleCustomRepeat(6, GameName, message, 3600f * (8f * 6f));
            ScheduleCustomRepeat(7, GameName, message, 3600f * (8f * 7f));
            ScheduleCustomRepeat(8, GameName, message, 3600f * (8f * 8f));
            ScheduleCustomRepeat(9, GameName, message, 3600f * (8f * 9f));
            ScheduleCustomRepeat(10, GameName, message, 3600f * (8f * 10f));
            ScheduleCustomRepeat(11, GameName, message, 3600f * (8f * 11f));
            ScheduleCustomRepeat(12, GameName, message, 3600f * (8f * 12f));
            ScheduleCustomRepeat(13, GameName, message, 3600f * (8f * 13f));
            ScheduleCustomRepeat(14, GameName, message, 3600f * (8f * 14f));
            ScheduleCustomRepeat(15, GameName, message, 3600f * (8f * 15f));



            ScheduleCustomRepeat(16, GameName, message, 3600f * (8f * 16f));
            ScheduleCustomRepeat(17, GameName, message, 3600f * (8f * 17f));
            ScheduleCustomRepeat(18, GameName, message, 3600f * (8f * 18f));
            ScheduleCustomRepeat(19, GameName, message, 3600f * (8f * 19f));
            ScheduleCustomRepeat(20, GameName, message, 3600f * (8f * 20f));
            ScheduleCustomRepeat(21, GameName, message, 3600f * (8f * 21f));
            ScheduleCustomRepeat(22, GameName, message, 3600f * (8f * 22f));
            ScheduleCustomRepeat(23, GameName, message, 3600f * (8f * 23f));
            ScheduleCustomRepeat(24, GameName, message, 3600f * (8f * 24f));
            ScheduleCustomRepeat(25, GameName, message, 3600f * (8f * 25f));
            ScheduleCustomRepeat(26, GameName, message, 3600f * (8f * 26f));
            ScheduleCustomRepeat(27, GameName, message, 3600f * (8f * 27f));
            ScheduleCustomRepeat(28, GameName, message, 3600f * (8f * 28f));
            ScheduleCustomRepeat(29, GameName, message, 3600f * (8f * 29f));
            ScheduleCustomRepeat(30, GameName, message, 3600f * (8f * 30f));
#endif
        }


        public void Rate()
        {
            Application.OpenURL("http://u3d.as/y6r");
        }

        public void OpenWiki()
        {
            //Application.OpenURL("https://github.com/hippogamesunity/SimpleAndroidNotificationsPublic/wiki");
        }

        public void ScheduleSimple()
        {
            NotificationManager.Send(TimeSpan.FromSeconds(5), "Simple notification", "Customize icon and color", new Color(1, 0.3f, 0.15f));
        }

        public void ScheduleNormal()
        {
            NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(84000), "Crowd Jump", "Lets Make High Score!", new Color(0, 0.6f, 1), NotificationIcon.Message);

        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                SendNotification();
        }
        public void ScheduleCustom()
        {
            var notificationParams = new NotificationParams
            {
                Id = 20,//UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(84000 / 2),// after 24hours
                Title = "Crowd Jump",
                Message = "Lets Make High Score!",
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Message,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }
        public void ScheduleCustom_Hours(int id, int hour)
        {
            var notificationParams = new NotificationParams
            {
                Id = id,//UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(3600f * hour),
                Title = "Soccer",
                Message = "Next Match Availible Now!",
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Star,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"

            };

            NotificationManager.SendCustom(notificationParams);
        }
        public void ScheduleCustom_Days(int id, int day)
        {
            var notificationParams = new NotificationParams
            {
                Id = id,//UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(84000 * day),// after 24hours
                Title = "Crowd Jump",
                Message = "Super Difficult Challenge Availible Now!",
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Star,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }

        public void ScheduleCustomRepeat(int id, string title, string message, float seconds)
        {
            var notificationParams = new NotificationParams
            {
                Id = id,
                Delay = TimeSpan.FromSeconds(seconds),
                Title = title,
                Message = message,
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Message,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }
        public void CancelAll()
        {
            //NotificationManager.CancelAll();
        }
    }
}