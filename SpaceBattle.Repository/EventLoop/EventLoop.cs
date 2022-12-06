using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Repository.EventLoop
{
	public class EventLoop
	{
        private bool Running { get; set; }
        public ConcurrentQueue<ICommand> ActionQueue { get; set; }
        public System.Threading.ManualResetEventSlim Wait { get; set; }

        private CancellationToken cancleToken;

        private CancellationTokenSource cancelTokenSource;
        public EventLoop()
        {
            ActionQueue = new ConcurrentQueue<ICommand>();
            Wait = new ManualResetEventSlim(false);

            cancelTokenSource = new CancellationTokenSource();
            cancleToken = cancelTokenSource.Token;
        }

        public void Loop()
        {
            while (Running)
            {
                if (ActionQueue.TryDequeue(out var action))
                {
                    try
                    {
                        Task.Factory.StartNew(() => action.Execute());
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    if (cancleToken.IsCancellationRequested && ActionQueue.IsEmpty)
                    {
                        break;
                    }

                    Wait.Reset();
                    Wait.Wait();
                }
            }
        }

        /// <summary>
        /// Добавление в очередь с пробуждением
        /// </summary>
        public void EnqueueWithAwakening(ICommand action)
        {
            ActionQueue.Enqueue(action);
            Wait.Set();
        }

        /// <summary>
        /// Добавление в очередь без пробуждения
        /// </summary>
        public void EnqueueWithoutAwakening(ICommand action)
        {
            ActionQueue.Enqueue(action);
        }
        public void Start()
        {
            Running = true;
            Wait.Set();
            Loop();
        }

        public void SoftStop()
        {
            cancelTokenSource.Cancel();
            Wait.Set();
        }

        public void HardStop()
        {
            Running = false;
            Wait.Set();
        }

    }
}
