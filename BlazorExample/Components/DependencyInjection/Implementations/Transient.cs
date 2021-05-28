using System;
using System.Threading;
using BlazorExample.Components.DependencyInjection.Contracts;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class Transient : ITransient, IDisposable
    {
        private static volatile int _previousInstanceNumber;

        public Transient()
        {
            Console.WriteLine("Transient Created");
            InstanceNumber = Interlocked.Increment(ref _previousInstanceNumber);
        }

        public void Dispose()
        {
            Console.WriteLine("Transient Disposed");
        }

        public int InstanceNumber { get; }
    }
}