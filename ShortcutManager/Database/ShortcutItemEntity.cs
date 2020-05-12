using System.Windows.Input;
using ShortcutManager.Enum;

namespace ShortcutManager.Database
{
    public class ShortcutItemEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ExecuteString { get; set; }

        public byte[] Icon { get; set; }

        public ShortcutItemType Type { get; set; }

        public Key Key { get; set; }

        public ModifierKeys ModifierKeys { get; set; }
    }
}