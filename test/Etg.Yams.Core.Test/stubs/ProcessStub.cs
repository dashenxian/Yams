﻿using System;
using System.Threading.Tasks;
using Etg.Yams.Process;

namespace Etg.Yams.Test.stubs
{
    public class ProcessStub : IProcess
    {
        private readonly string _exePath;
        private string _exeArgs;

        public ProcessStub(string exePath)
        {
            _exePath = exePath;
            ShouldStart = true;
        }

        public string ExePath
        {
            get { return _exePath; }
        }

        public string ExeArgs
        {
            get { return _exeArgs; }
        }

        public Task Start(string exeArgs)
        {
            _exeArgs = exeArgs;
            if (!ShouldStart)
            {
                throw new Exception("Cannot start process");
            }
            IsRunning = true;
            return Task.FromResult(true);
        }

        public Task Close()
        {
            IsRunning = false;
            return Task.FromResult(true);
        }

        public Task Kill()
        {
            IsRunning = false;
            return Task.FromResult(true);
        }

        public Task ReleaseResources()
        {
            IsRunning = false;
            return Task.FromResult(true);
        }

        public bool HasExited
        {
            get { return !IsRunning; }
        }

        public bool IsRunning
        {
            get;
            private set;
        }

        public bool ShouldStart { get; set; }

        public void RaiseExitedEvent()
        {
            var failedEvent = Exited;
            if (failedEvent == null) return;
            failedEvent(this, new ProcessExitedArgs("process Exited event raised from stub"));
        }

        public event EventHandler<ProcessExitedArgs> Exited;
        public void Dispose()
        {
        }
    }
}
