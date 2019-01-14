using System;
using GalaSoft.MvvmLight;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using ATWService.Model;
using ATWService;

namespace ATWBox.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Task _readTask;
        private CancellationToken _cancellationToken;
        private List<ReadType> _reads { get; set; } = new List<ReadType>();

        public MainViewModel()
        {
            _readTask = new Task(() =>
            {
                _cancellationToken = new CancellationToken();
                //var binding = new BasicHttpBinding();
                var binding = new NetTcpBinding();
                var endpoint = new EndpointAddress(Consts.SERVICE_URL);
                using (var channelFactory = new ChannelFactory<IReadingService>(binding, endpoint))
                {
                    IReadingService service = null;
                    try
                    {
                        service = channelFactory.CreateChannel();
                        do
                        {
                            var read = service.GetReadUsingDataContract(new ReadType());
                            _reads.Add(read);

                            var reading = service.GetReadingUsingDataContract(new ReadingType());
                            var reader = service.GetReaderUsingDataContract(new ReaderType());

                            _readTask.Wait(Consts.DELAY);
                        } while (_cancellationToken.IsCancellationRequested == false);
                    }
                    catch (Exception ex)
                    {
                        (service as ICommunicationObject)?.Abort();
                    }
                }
            });

            _readTask.ConfigureAwait(false);
            _readTask.Start();
        }
    }
}