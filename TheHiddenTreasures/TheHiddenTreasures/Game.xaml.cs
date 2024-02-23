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

        private readonly Dictionary<VirtualKey, int> KEY_TO_TILE = new Dictionary<VirtualKey, int>()
            {
                { VirtualKey.S, 0 },
                { VirtualKey.D, 2 },
                { VirtualKey.W, 4 },
                { VirtualKey.A, 6 }
            };

        public Game()
        {
            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.666);
            timer.Tick += GameLoop;

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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var proxy = new ServiceReference1.Service1Client();
            int playerSkin = await proxy.GetPlayerCurrentSkinAsync(MainPage.username);

            handler = new Handler(playerSkin, ref GameCanvas, ref GameCamera, ref X_tb, ref Y_tb, ref Level_tb, ref Coins_tb, FinishGame);
            timer.Start();
        }

        private void TimeCounterTimer_Tick(object sender, object e)
        {
            time++;
            Time_tb.Text = $"Time: {time}";
        }

        private void GameLoop(object sender, object e)
        {
            HandleInput();
        }

        private void HandleInput()
        {
            List<VirtualKey> currentlyPressed = new List<VirtualKey>();

            // If any of the movement keys is pressed, move the player
            foreach(var key in MOVEMENT_KEYS)
            {
                if (KeyIsPressed(key))
                {
                    handler.GetPlayer().Move(key);
                    currentlyPressed.Add(key);
                }
            }

            ChangePlayerIdleImage(currentlyPressed);

            if (currentlyPressed.Count == 0 || handler.GetPlayer().DidAnimationDirectionChange())
                handler.GetPlayer().StopAnimation();
            else
                handler.GetPlayer().StartAnimation();
        }

        private void ChangePlayerIdleImage(List<VirtualKey> currentlyPressed)
        {
            if (currentlyPressed.Count == 0)
                return;

            if (currentlyPressed.Count == 1)
            {
                handler.GetPlayer().ChangeIdleDirection(KEY_TO_TILE[currentlyPressed[0]]);
                return;
            }

            int t1 = KEY_TO_TILE[currentlyPressed[0]], t2 = KEY_TO_TILE[currentlyPressed[1]];

            if (Math.Abs(t1 - t2) == 4)
                return;

            int tile = Math.Abs(t1 - t2) == 6 ? 7 : (t1 + t2) / 2;
            handler.GetPlayer().ChangeIdleDirection(tile);
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
                    foreach (var obj in handler.RenderObjectList)
                        obj.Rect.Opacity = 1;
                }
            }
        }

        private async void FinishGame(bool didWin, int coins)
        {
            // Free this method from the handler of the KeyDown event
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;

            string userMsg = didWin ? "You Won!" : "You Lose!";
            var dialog = new MessageDialog(userMsg);
            await dialog.ShowAsync();

            StoreStatistics(didWin, coins);
            Frame.Navigate(typeof(MainPage));
        }

        private async void StoreStatistics(bool didWin, int coins)
        {
            var proxy = new ServiceReference1.Service1Client();

            if(!await proxy.UpdateStatisticsAsync(MainPage.username, didWin, didWin ? time : 0, coins))
            {
                var dialog = new MessageDialog("Error while trying to store statistics");
                await dialog.ShowAsync();
            }
        }
    }
}
