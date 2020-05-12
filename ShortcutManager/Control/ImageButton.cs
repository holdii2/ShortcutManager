
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.IconPacks;

namespace ShortcutManager.Control
{
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        public static readonly DependencyProperty IsHoveredProperty = DependencyProperty.Register(
            nameof(IsHovered), typeof(bool), typeof(ImageButton), new PropertyMetadata(default(bool)));

        public bool IsHovered
        {
            get => (bool) GetValue(IsHoveredProperty);
            set => SetValue(IsHoveredProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(PackIconMaterial), typeof(ImageButton), new PropertyMetadata(default(PackIconMaterial)));

        public PackIconMaterial Icon
        {
            get => (PackIconMaterial) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            IsHovered = true;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            IsHovered = false;
        }
    }
}