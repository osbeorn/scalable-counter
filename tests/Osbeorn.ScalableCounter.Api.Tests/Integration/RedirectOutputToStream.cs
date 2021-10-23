using System;
using System.IO;
using DotNet.Testcontainers.Containers.OutputConsumers;

namespace Osbeorn.ScalableCounter.Api.Tests.Integration
{
    public class RedirectOutputToStream : IOutputConsumer
    {
        private readonly StreamWriter _stdout;

        private readonly StreamWriter _stderr;
        
        public RedirectOutputToStream() 
            : this(Console.OpenStandardOutput(), Console.OpenStandardError())
        {
        }
        
        public RedirectOutputToStream(Stream stdout, Stream stderr)
        {
            this._stdout = new StreamWriter(stdout) { AutoFlush = true };
            this._stderr = new StreamWriter(stderr) { AutoFlush = true };
        }
        
        public Stream Stdout
            => _stdout.BaseStream;
        
        public Stream Stderr
            => _stderr.BaseStream;
        
        public void Dispose()
        {
            _stdout.Dispose();
            _stderr.Dispose();
        }
    }
}