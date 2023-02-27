using UnityEngine.Events;
using Agava.YandexGames;

namespace Factory
{
    public static class Advertizment
    {
        public static event UnityAction AdShow;
        public static event UnityAction AdClose;
        public static event UnityAction Rewarded;

        public static void ShowInterstitialAd()
        {
            InterstitialAd.Show(onOpenCallback: OnOpenCallback, onCloseCallback: OnCloseCallback, onErrorCallback: OnErrorCallback);
        }

        public static void ShowRewardedAd()
        {
            VideoAd.Show(onOpenCallback: OnOpenCallback, onRewardedCallback: OnRewardedCallback, onCloseCallback: OnCloseCallback, onErrorCallback: OnErrorCallback);
        }

        private static void OnRewardedCallback() => Rewarded?.Invoke();

        private static void OnCloseCallback() => AdClose?.Invoke();

        private static void OnCloseCallback(bool obj) => AdClose?.Invoke();

        private static void OnErrorCallback(string obj) => AdClose?.Invoke();

        private static void OnOpenCallback() => AdShow?.Invoke();
    }
}