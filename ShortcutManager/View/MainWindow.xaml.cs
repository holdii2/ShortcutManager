using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interop;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore.Internal;
using ShortcutManager.Classes;
using ShortcutManager.Database;
using ShortcutManager.ViewModel;

namespace ShortcutManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private HwndSource source;

        public void RegisterHotKey(ShortcutItemEntity item)
        {
            var helper = new WindowInteropHelper(this);
            if (!ExternalFunctions.RegisterHotKey(helper.Handle, item.Id+9000, (uint)item.ModifierKeys, (uint)item.Key))
            {
                throw new Exception("Could not register Shortcut");
            }
        }

        public void UnRegisterHotKey(ShortcutItemEntity item)
        {
            var helper = new WindowInteropHelper(this);
            ExternalFunctions.UnregisterHotKey(helper.Handle, item.Id);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            source.RemoveHook(HwndHook);
            source = null;

            if (!(DataContext is MainViewModel vm))
            {
                base.OnClosing(e);
                return;
            }

            foreach (var shortcutItem in vm.Shortcuts)
            {
                UnRegisterHotKey(shortcutItem.ShortcutItemEntity);
            }

            base.OnClosing(e);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var helper = new WindowInteropHelper(this);
            source = HwndSource.FromHwnd(helper.Handle);
            source?.AddHook(HwndHook);

            if (!(DataContext is MainViewModel vm))
                return;

            vm.window = this;

            vm.Load();

        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    var vm = DataContext as MainViewModel;
                    var shortCut = vm?.Shortcuts.FirstOrDefault(i => (int)i.ShortcutItemEntity.Id + 9000 == wParam.ToInt32());

                    if (shortCut == null)
                        return IntPtr.Zero;

                    shortCut.Execute();
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
