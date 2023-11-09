using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
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
        public static bool isCameraOn;

        private Handler handler;
        private DispatcherTimer timer;

        private readonly List<Windows.System.VirtualKey> MOVEMENT_KEYS;

        public Game()
        {
            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.666);
            timer.Tick += GameLoop;
            timer.Start();

            MOVEMENT_KEYS = new List<Windows.System.VirtualKey>()
            { Windows.System.VirtualKey.W , Windows.System.VirtualKey.A, Windows.System.VirtualKey.S, Windows.System.VirtualKey.D};

            isCameraOn = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            handler = new Handler(ref GameCanvas, ref GameCamera);
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
                    handler.FocusOnPlayer();
                else
                    GameCamera.GlobalOffsetZ = -25 * Handler.ZOOM_LEVEL;
            }
        }
    }
}
