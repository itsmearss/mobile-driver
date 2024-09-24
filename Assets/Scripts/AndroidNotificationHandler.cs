using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string channelId = "notification_channel";

    public void ScheduleNotification(DateTime datetime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = channelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Ready",
            Text = "Your energy is ready to play!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = datetime,
        };

        AndroidNotificationCenter.SendNotification(notification, channelId);
    }
#endif
}
