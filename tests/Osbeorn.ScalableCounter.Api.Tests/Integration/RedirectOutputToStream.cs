using System;
using System.IO;
using DotNet.Testcontainers.Containers.OutputConsumers;

namespace Osbeorn.ScalableCounter.Api.Tests
{
    public class RedirectOutputToStream : IOutputConsumer
    {
        private readonly StreamWriter stdout;

        private readonly StreamWriter stderr;
        
        public RedirectOutputToStream() 
            : this(Console.OpenStandardOutput(), Console.OpenStandardError())
        {
        }
        
        public RedirectOutputToStream(Stream stdout, Stream stderr)
        {
            this.stdout = new StreamWriter(stdout) { AutoFlush = true };
            this.stderr = new StreamWriter(stderr) { AutoFlush = true };
        }
        
        public Stream Stdout
            => this.stdout.BaseStream;
        
        public Stream Stderr
            => this.stderr.BaseStream;
        
        public void Dispose()
        {
            this.stdout.Dispose();
            this.stderr.Dispose();
        }
    }
}