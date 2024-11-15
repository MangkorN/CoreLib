using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CoreLib.Utilities;

namespace CoreLib.UnitTest.Utilities
{
    public class CounterStateUtilitiesTest
    {
        private int _counter;
        private bool _stateChanged;
        private bool _stateError;

        [Test]
        public void IncrementCounter_IncreasesCounterByOne()
        {
            _counter = 0;
            CounterStateUtilities.ProcessCounterState(ref _counter, true, null, null);
            Assert.AreEqual(1, _counter);
        }

        [Test]
        public void DecrementCounter_DecreasesCounterByOne()
        {
            _counter = 1;
            CounterStateUtilities.ProcessCounterState(ref _counter, false, null, null);
            Assert.AreEqual(0, _counter);
        }

        [Test]
        public void IncrementCounterToOne_InvokesOnStateChange()
        {
            _counter = 0;
            _stateChanged = false;
            CounterStateUtilities.ProcessCounterState(ref _counter, true, () => _stateChanged = true, null);
            Assert.IsTrue(_stateChanged);
        }

        [Test]
        public void DecrementCounterToZero_InvokesOnStateChange()
        {
            _counter = 1;
            _stateChanged = false;
            CounterStateUtilities.ProcessCounterState(ref _counter, false, () => _stateChanged = true, null);
            Assert.IsTrue(_stateChanged);
        }

        [Test]
        public void DecrementCounterBelowZero_InvokesOnStateError()
        {
            _counter = 0;
            _stateError = false;
            CounterStateUtilities.ProcessCounterState(ref _counter, false, null, () => _stateError = true);
            Assert.IsTrue(_stateError);
        }
    }
}
