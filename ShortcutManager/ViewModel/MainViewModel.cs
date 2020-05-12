using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using ShortcutManager.Annotations;
using ShortcutManager.Classes; 
using ShortcutManager.Database;
using ShortcutManager.Enum;
using ShortcutManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShortcutManager.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GlobalKeyboardHook globalKeyboardHook;

        public void Load()
        {
            using var ctx = new DatabaseContext();
            ctx.Database.Migrate();
            RefreshView();
        }

        private ObservableCollection<ShortcutItem> shortcuts;
        private ShortcutItem selectedShortcut;

        internal MainWindow window;

        public ObservableCollection<ShortcutItem> Shortcuts
        {
            get => shortcuts;
            set
            {
                if (Equals(value, shortcuts)) return;
                shortcuts = value;
                OnPropertyChanged();
            }
        }

        public ShortcutItem SelectedShortcut
        {
            get => selectedShortcut;
            set
            {
                if (Equals(value, selectedShortcut))
                    return;

                selectedShortcut = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddShortcutCommand => new RelayCommand(AddShortcut);

        public RelayCommand ExecuteShortcutCommand => new RelayCommand(ExecuteShortcut);

        public RelayCommand DeleteShortcutCommand => new RelayCommand(DeleteShortcut);

        public RelayCommand EditShortcutCommand  => new RelayCommand(EditShortcut);

        private void EditShortcut()
        {
            if (selectedShortcut == null)
                return;

            new AddShortcutWindow(this, selectedShortcut.ShortcutItemEntity).Show();
        }

        private void DeleteShortcut()
        {
            if (selectedShortcut == null)
                return;

            using var ctx = new DatabaseContext();

            ctx.Shortcuts.Remove(SelectedShortcut.ShortcutItemEntity);

            ctx.SaveChanges();

            RefreshView();
        }

        private void ExecuteShortcut()
        {
            SelectedShortcut.Execute();
        }

        private void AddShortcut()
        {
            new AddShortcutWindow(this).Show();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshView()
        {
            //globalKeyboardHook?.Unhook();
            LoadShortcuts();
        }

        private void LoadShortcuts()
        {
            using var ctx = new DatabaseContext();
            var result = new List<ShortcutItem>();

            foreach (var shortcutItemEntity in ctx.Shortcuts)
            {
                switch (shortcutItemEntity.Type)
                {
                    case ShortcutItemType.Exe:
                        result.Add(new ExeFileShortcutItem(shortcutItemEntity));
                        break;
                    case ShortcutItemType.Code:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Shortcuts = new ObservableCollection<ShortcutItem>(result);

            globalKeyboardHook = new GlobalKeyboardHook
            {
                HookedKeys = Shortcuts.Select(i => (Keys) KeyInterop.VirtualKeyFromKey(i.ShortcutItemEntity.Key))
                    .ToList()
            };

            globalKeyboardHook.KeyPressed += XOnKeyPressed;
        }

        private void XOnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            var shortCut = Shortcuts.FirstOrDefault(i =>
                (Keys) KeyInterop.VirtualKeyFromKey(i.ShortcutItemEntity.Key) == e.Key &&
                (int) i.ShortcutItemEntity.ModifierKeys == (int) e.Modifier);

            shortCut?.Execute();
        }
    }
}