using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheHiddenTreasures
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        public static bool isCameraOn, isVisibilityOn;

        private Handler handler;
        private DispatcherTimer timer;

        private int time;
        private DispatcherTimer timeCounterTimer;

        private readonly List<Windows.System.VirtualKey> MOVEMENT_KEYS;

        public Game()
        {
            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.666);
            timer.Tick += GameLoop;
            timer.Start();

            time = 0;
            timeCounterTimer = new DispatcherTimer();
            timeCounterTimer.Interval = TimeSpan.FromSeconds(1);
            timeCounterTimer.Tick += TimeCounterTimer_Tick;
            timeCounterTimer.Start();

            MOVEMENT_KEYS = new List<Windows.System.VirtualKey>()
            { Windows.System.VirtualKey.W , Windows.System.VirtualKey.A, Windows.System.VirtualKey.S, Windows.System.VirtualKey.D};

            isCameraOn = true;
            isVisibilityOn = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            handler = new Handler(ref GameCanvas, ref GameCamera, ref X_tb, ref Y_tb, ref Level_tb, FinishGame);
        }

        private void TimeCounterTimer_Tick(object sender, object e)
        {
            time++;
        }

        private void GameLoop(object sender, object e)
        {
            HandleInput();
        }

        private void HandleInput()
        {
            // If any of the movement keys is pressed, move the player
            foreach(var key in MOVEMENT_KEYS)
            {
                if (KeyIsPressed(key))
                    handler.GetPlayer().Move(key);
            }
        }

        private bool KeyIsPressed(Windows.System.VirtualKey key)
        {
            CoreVirtualKeyStates state = CoreWindow.GetForCurrentThread().GetKeyState(key);
            return (state & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            // If the key pressed is one of the movement keys, just ignore it in this function
            if (MOVEMENT_KEYS.Count(x => x == args.VirtualKey) != 0)
                return;

            // Toggle camera
            if(args.VirtualKey == Windows.System.VirtualKey.C)
            {
                isCameraOn = !isCameraOn;

                if (isCameraOn)
                    handler.UpdateOnPlayerMove();
                else
                    GameCamera.GlobalOffsetZ = -25 * Handler.ZOOM_LEVEL;
            }
            else if(args.VirtualKey == Windows.System.VirtualKey.X)
            {
                isVisibilityOn = !isVisibilityOn;

                if (isVisibilityOn)
                    handler.UpdateOnPlayerMove();
                else
                {
                    foreach (var obj in handler.RenderObjectLst)
                        obj.Rect.Opacity = 1;
                }
            }
        }

        private void FinishGame(bool didWin)
        {
            StoreStatistics(didWin);

            // Free this method from the handler of the KeyDown event
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;

            handler = new Handler(ref GameCanvas, ref GameCamera, ref X_tb, ref Y_tb, ref Level_tb, FinishGame);
            Frame.Navigate(typeof(MainPage));
        }

        private async void StoreStatistics(bool didWin)
        {
            var proxy = new ServiceReference1.Service1Client();

            if(!await proxy.UpdateStatisticsAsync(MainPage.username, didWin, time))
            {
                var dialog = new MessageDialog("Error while trying to store statistics");
                await dialog.ShowAsync();
            }
        }
    }
}
