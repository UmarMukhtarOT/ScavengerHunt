using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;


public class FbAnalytics : MonoBehaviour
{
    public static FbAnalytics Instance;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    public bool firebaseInitialized = false;
    private Queue<string> logEvents;
    public bool IsLogEnabled = true;

    public void Awake()
    {
#if UNITY_EDITOR

        Debug.unityLogger.logEnabled = true;

#else

Debug.unityLogger.logEnabled = false;

#endif
        Instance = this;
        DontDestroyOnLoad(gameObject);
        logEvents = new Queue<string>();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {

            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();

            }
            else
            {
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {

        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        // Set the user's sign up method.
        firebaseInitialized = true;
        /*RemoteValues.Instance.iniliaze();  Abdullah*/

    }







    /// <summary>
    /// any event detail of Analytics will pass through here
    /// </summary>
    /// <param name="info"></param>
    public void LogEvent(string info)
    {
        //if (Application.isEditor)
            Debug.Log("<color=red>" + info + "</color>");
        if (firebaseInitialized)
        {
            FirebaseAnalytics.LogEvent(info);
            //  Debug.LogError(
            //  info);
        }
    ;
    }

    /// <summary>
    /// log with int parameter
    /// </summary>
    /// <param name="info"></param>
    /// <param name="parameterName"></param>
    /// <param name="parameterValue"></param>
    public void LogEvent(string info, string parameterName, int parameterValue)
    {
        if (firebaseInitialized)
        {
            FirebaseAnalytics.LogEvent(info, parameterName, parameterValue);
            //   Debug.LogError(
            // info + parameterName + parameterValue);
        }
    }

    //    public void 







    // Reset analytics data for this app instance.


}