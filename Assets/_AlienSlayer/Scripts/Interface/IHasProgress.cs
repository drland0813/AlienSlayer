using System;

namespace drland.AlienSlayer
{
    public interface IHasProgress
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float CurrentValue;
            public float ProgressNormalized;
        }
    }
}