﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WindowsReddit
{
    public interface IIncrementalSource<T>
    {
        Task<IEnumerable<T>> GetPagedItems(int pageIndex, int pageSize);
    }
    public class IncrementalLoadingCollection<T, I> : ObservableCollection<I>,
     ISupportIncrementalLoading
     where T : IIncrementalSource<I>, new()
    {
        private T source;
        private int itemsPerPage;
        private bool hasMoreItems;
        private int currentPage;
        public IncrementalLoadingCollection(int itemsPerPage = 20)
        {
            this.source = new T();
            this.itemsPerPage = itemsPerPage;
            this.hasMoreItems = true;
        }

        public bool HasMoreItems
        {
            get { return hasMoreItems; }
        }

        private async Task<LoadMoreItemsResult> LoadDataAsync()
        {
            var dispatcher = Window.Current.Dispatcher;

            uint resultCount = 0;
            var result = await source.GetPagedItems(currentPage++, itemsPerPage);

            if (result == null || result.Count() == 0)
            {
                hasMoreItems = false;
            }
            else
            {
                resultCount = (uint)result.Count();

                await dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        foreach (I item in result)
                            this.Add(item);
                    });
            }

            return new LoadMoreItemsResult() { Count = resultCount };
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadDataAsync().AsAsyncOperation<LoadMoreItemsResult>();
        }
    }
}
