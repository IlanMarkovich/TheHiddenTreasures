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
        private Handler handler;
        private DispatcherTimer timer;

        public Game()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.666);
            timer.Tick += GameLoop;
            timer.Start();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            handler = new Handler(GameCanvas);
        }

        private void GameLoop(object sender, object e)
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (KeyIsPressed(Windows.System.VirtualKey.W))
                handler.GetPlayer().MoveUp();

            if (KeyIsPressed(Windows.System.VirtualKey.A))
                handler.GetPlayer().MoveLeft();

            if (KeyIsPressed(Windows.System.VirtualKey.S))
                handler.GetPlayer().MoveDown();

            if (KeyIsPressed(Windows.System.VirtualKey.D))
                handler.GetPlayer().MoveRight();
        }

        private bool KeyIsPressed(Windows.System.VirtualKey key)
        {
            CoreVirtualKeyStates state = CoreWindow.GetForCurrentThread().GetKeyState(key);
            return (state & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }
    }
}
