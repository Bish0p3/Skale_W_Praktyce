using Xamarin.Essentials;

public static class Settings
{
    public static bool FirstRun
    {
        get => Preferences.Get(nameof(FirstRun), true);
        set => Preferences.Set(nameof(FirstRun), value);
    }
    public static int CurrentUserID
    {
        get => Preferences.Get(nameof(CurrentUserID), 0);
        set => Preferences.Set(nameof(CurrentUserID), value);
    }
}
