using System.Windows.Input;
using MahApps.Metro.Controls;
using ShortcutManager.Database;
using ShortcutManager.ViewModel;

namespace ShortcutManager.View
{
    /// <summary>
    /// Interaktionslogik für AddShortcutWindow.xaml
    /// </summary>
    public partial class AddShortcutWindow : MetroWindow
    {
        public AddShortcutWindow(MainViewModel mainViewModel)
        {
            DataContext = new AddShortcutViewModel();
            Init(mainViewModel);
        }

        public AddShortcutWindow(MainViewModel mainViewModel, ShortcutItemEntity entity)
        {
            DataContext = new AddShortcutViewModel(entity);
            Init(mainViewModel);
        }

        private void Init(MainViewModel mainViewModel)
        {
            if (!(DataContext is AddShortcutViewModel vm))
                return;

            KeyDown += OnKeyDown;

            vm.mainViewModel = mainViewModel;

            vm.window = this;

            InitializeComponent();
        }

        private bool isSelectingShortcut;

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!isSelectingShortcut)
                return;

            if (!(DataContext is AddShortcutViewModel viewModel))
                return;

            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt || e.Key == Key.LeftShift || e.Key == Key.RightShift|| e.Key == Key.LWin || e.Key == Key.RWin || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                return;

            viewModel.Entity.ModifierKeys = Keyboard.Modifiers;

            viewModel.Entity.Key = e.Key;

            viewModel.OnPropertyChanged(nameof(viewModel.ShortcutString));

            isSelectingShortcut = false;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            isSelectingShortcut = true;
        }
    }
}
