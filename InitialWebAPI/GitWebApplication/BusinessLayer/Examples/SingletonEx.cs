using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Examples
{

    //thread safe
    public sealed class LazySingleton
    {
        private LazySingleton()
        {

        }

        public static LazySingleton Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            static Nested()
            {

            }

            internal static readonly LazySingleton instance = new LazySingleton();
        }
    }

    public sealed class SiteStructure
    {
        public static readonly SiteStructure instance = new SiteStructure();

        private SiteStructure()
        {
            // Initialize.
        }
    }

    public class LazyTSingleton
    {
        //In .NET Framework 4.0, the Lazy<T> class was introduced, which internally uses double-checked locking by default (ExecutionAndPublication mode) 
        //to store either the exception that was thrown during construction, or the result of the function that was passed to Lazy<T>
        private static readonly Lazy<LazyTSingleton> instance = new Lazy<LazyTSingleton>(() => new LazyTSingleton());

        private LazyTSingleton() { }

        public static LazyTSingleton Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }

    // Not thread-safe
    public class SingletonEx
    {
        private static SingletonEx instance;

        private SingletonEx()
        {

        }

        public static SingletonEx Instance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonEx();

                return instance;
            }
        }

        public void DoStuff() { }
    }

    public class SampleSingletonUsage
    {
        public void SomeMethod()
        {
            SingletonEx.Instance.DoStuff();

            var myObject = SingletonEx.Instance;
            myObject.DoStuff();

            SomeOtherMethod(SingletonEx.Instance);
        }

        private void SomeOtherMethod(SingletonEx singleton)
        {
            singleton.DoStuff();
        }
    }
}
