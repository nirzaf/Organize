using Microsoft.AspNetCore.Components;
using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ObservableCollectionObserver<TProp> : ComponentBase, IDisposable where TProp : NotifyingObject
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public ObservableCollection<TProp> Collection { get; set; }

        [Parameter]
        public bool ObserveChildren { get; set; } = false;

        private ObservableCollection<TProp> _collectionToObserve;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (_collectionToObserve != null)
            {
                UnRegisterCollectionToObserve();
            }

            if (Collection == null)
            {
                return;
            }

            _collectionToObserve = Collection;
            _collectionToObserve.CollectionChanged += HandleCollectionChanged;
            if (ObserveChildren)
            {
                foreach (var notifyingObject in _collectionToObserve)
                {
                    notifyingObject.PropertyChanged += HandlePropertyChanged;
                }
            }
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && ObserveChildren)
            {
                var newItems = e.NewItems.OfType<NotifyingObject>();
                foreach (var notifyingObject in newItems)
                {
                    notifyingObject.PropertyChanged += HandlePropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                var oldItems = e.OldItems.OfType<NotifyingObject>();
                foreach (var notifyingObject in oldItems)
                {
                    notifyingObject.PropertyChanged -= HandlePropertyChanged;
                }
            }

            StateHasChanged();
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        private void UnRegisterCollectionToObserve()
        {
            _collectionToObserve.CollectionChanged -= HandleCollectionChanged;
            foreach (var notifyingObject in _collectionToObserve)
            {
                notifyingObject.PropertyChanged -= HandlePropertyChanged;
            }
        }

        public void Dispose()
        {
            UnRegisterCollectionToObserve();
        }
    }
}
