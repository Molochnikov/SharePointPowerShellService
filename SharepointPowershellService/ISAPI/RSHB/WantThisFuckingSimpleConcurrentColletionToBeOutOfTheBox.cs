using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FuckingExternalWrapperToSuppressCompilerError;

namespace SharepointPowershellService.ISAPI.RSHB
{

    public class WantThisFuckingSimpleConcurrentColletionToBeOutOfTheBox<FuckingExternalWrapperToSuppressCompilerErrorFuckingExternalStruct> : IProducerConsumerCollection<FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct>
    {
        private SynchronizedCollection<FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct> runspaces = new SynchronizedCollection<FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct>();
        public int Count => runspaces.Count;

        public object SyncRoot => runspaces.SyncRoot;

        public bool IsSynchronized => throw new NotImplementedException();



        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct> GetEnumerator()
        {
            return runspaces.GetEnumerator();
        }

        public FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct[] ToArray()
        {
            return runspaces.ToArray();
        }

        public bool TryAdd(FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct item)
        {
            runspaces.Add(item);
            return true;
        }





        //https://dzone.com/articles/should-we-initialize-an-out-parameter-before-a-met
        public bool TryTake(out FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct item)
        {
            var result = TryTakeA(item);
            //item = default; //thanks god now i don't need this. fuck you c# compiler!
            return result;
        }



        public bool TryTakeA(dynamic item)
        {
            return runspaces.Remove(item);
        }

        void IProducerConsumerCollection<FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct>.CopyTo(FuckingExternalWrapperToSuppressCompilerError.FuckingExternalStruct[] array, int index)
        {
            throw new NotImplementedException();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return runspaces.GetEnumerator();
        }


    }
}
