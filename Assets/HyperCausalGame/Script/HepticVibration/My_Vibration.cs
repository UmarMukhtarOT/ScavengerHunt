

public static class My_Vibration
{

    public static void Vibrate_It(long strength)
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        Vibration.Vibrate(strength);
#endif
    }
}
