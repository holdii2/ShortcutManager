using GalaSoft.MvvmLight.Command;
using ShortcutManager.Annotations;
using ShortcutManager.Database;
using ShortcutManager.View;
using System.ComponentModel;
using System.Linq;

namespace ShortcutManager.ViewModel
{
    public class AddShortcutViewModel : INotifyPropertyChanged
    {
        internal MainViewModel mainViewModel;

        internal AddShortcutWindow window;
        public AddShortcutViewModel()
        {
            Entity = new ShortcutItemEntity();
        }

        public AddShortcutViewModel(ShortcutItemEntity entity)
        {
            Entity = entity;
        }

        private ShortcutItemEntity entity;

        public ShortcutItemEntity Entity
        {
            get => entity;
            set
            {
                if (Equals(value, entity)) return;
                entity = value;
                OnPropertyChanged(nameof(Entity));
            }
        }

        public RelayCommand SaveEntityCommand => new RelayCommand(SaveEntity);

        public RelayCommand CancelCommand => new RelayCommand(Cancel);

        private void Cancel()
        {
            window.Close();
        }

        public string ShortcutString => $"{Entity.ModifierKeys} + {Entity.Key}";

        private void SaveEntity()
        {
            using var context = new DatabaseContext();

            var existingEntity = context.Shortcuts.FirstOrDefault(i => i.Id == Entity.Id);

            if (existingEntity != null)
            {
                existingEntity.ExecuteString = Entity.ExecuteString;
                existingEntity.Icon = Entity.Icon;
                existingEntity.Key = Entity.Key;
                existingEntity.ModifierKeys = Entity.ModifierKeys;
                existingEntity.Title = Entity.Title;
                existingEntity.Type = Entity.Type;

                context.SaveChanges();
            }
            else
            {
                Entity.Id = context.GetNextShortcutId();

                context.Shortcuts.Add(Entity);
            }

            context.SaveChanges();

            mainViewModel?.RefreshView();

            window?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}