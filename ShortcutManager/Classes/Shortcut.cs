using System.Windows.Input;

namespace ShortcutManager.Classes
{
    public class Shortcut
    {
        public Key Key { get; set; }

        public ModifierKeys ModifierKey { get; set; }
    }
}