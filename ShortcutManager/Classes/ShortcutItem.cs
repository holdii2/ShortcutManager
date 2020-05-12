using ShortcutManager.Annotations;
using System.ComponentModel;
using ShortcutManager.Database;

namespace ShortcutManager.Classes
{
    public abstract class ShortcutItem : INotifyPropertyChanged
    {
        protected ShortcutItem(ShortcutItemEntity shortcutItemEntity)
        {
            this.shortcutItemEntity = shortcutItemEntity;
        }

        private ShortcutItemEntity shortcutItemEntity;

        public ShortcutItemEntity ShortcutItemEntity
        {
            get => shortcutItemEntity;
            set
            {
                if (Equals(value, shortcutItemEntity)) return;
                shortcutItemEntity = value;
                OnPropertyChanged(nameof(ShortcutItemEntity));
            }
        }

        public abstract void Execute();


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}