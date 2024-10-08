using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pxic.DynamicUI.Helper
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify subscribers that a property value has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed. If not provided, 
        /// it defaults to the name of the calling property.</param>
        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
