using System;
using System.Diagnostics;
using ShortcutManager.Database;

namespace ShortcutManager.Classes
{
    public class ExeFileShortcutItem : ShortcutItem
    {
        public override void Execute()
        {
            try
            {
                Process.Start(ShortcutItemEntity.ExecuteString);
            }
            catch (Exception e)
            {
                
            }
            
        }

        public ExeFileShortcutItem(ShortcutItemEntity shortcutItemEntity) : base(shortcutItemEntity)
        {
        }
    }
}