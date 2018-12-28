using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff.Network
{
    public static class TcpListenerExtensions
    {
        public static async Task<TcpClient> AcceptTcpClientAsync(this TcpListener listener, CancellationToken ct)
        {
            using (ct.Register(listener.Stop))
            {
                try
                {
                    return await listener.AcceptTcpClientAsync();
                }
                catch (SocketException e)
                {
                    Console.WriteLine($"{e.Message} - {e.SocketErrorCode}");
                    if (e.SocketErrorCode == SocketError.Interrupted)
                    {
                        throw new OperationCanceledException();
                    }

                    throw;
                }
                catch (ObjectDisposedException) when (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Cancellation requested - Object disposed exception");
                    throw new OperationCanceledException();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }
        }
    }
}
