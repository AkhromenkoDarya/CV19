using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {
        private WeakReference _targetObjectReference;

        private WeakReference _rootObjectReference;

        private bool _disposed;

        public object TargetObject => _targetObjectReference;

        public object RootObject => _rootObjectReference;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
            var handlers = PropertyChanged;

            if (handlers is null)
            {
                return;
            }

            var invocationList = handlers.GetInvocationList();
            var arg = new PropertyChangedEventArgs(propertyName);

            foreach (Delegate action in invocationList)
            {
                if (action.Target is DispatcherObject dispatcherObject)
                {
                    dispatcherObject.Dispatcher.Invoke(action, this, arg);
                }
                else
                {
                    action.DynamicInvoke(this, arg);
                }
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName 
            = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var targetValueService = serviceProvider.GetService(typeof(IProvideValueTarget))
                as IProvideValueTarget;
            var rootObjectService = serviceProvider.GetService(typeof(IRootObjectProvider))
                as IRootObjectProvider;

            OnInitialized(targetValueService?.TargetObject, targetValueService?.TargetProperty,
                rootObjectService?.RootObject);

            return this;
        }

        protected virtual void OnInitialized(object target, object property, object root)
        {
            _targetObjectReference = new WeakReference(target);
            _rootObjectReference = new WeakReference(root);
        }

        //~ViewModel()
        //{
        //    Dispose(false);
        //}

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }

            _disposed = true;
            // Освобождение управляемых ресурсов.
        }
    }
}
