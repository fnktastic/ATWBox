using System;
using GalaSoft.MvvmLight;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using ATWService.Model;
using ATWService;
using ATWBox.Enum;

namespace ATWBox.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region fields
        private Task _readTask;
        private CancellationToken _cancellationToken;
        private List<ReadType> _reads { get; set; } = new List<ReadType>();
        private dynamic binding;
        private dynamic endpoint;
        #endregion

        #region constructor
        public MainViewModel()
        {
            try
            {
                Logger.InitLogger();
                InitProtocol(ProtocolEnum.Http);
                InitTask();
                _readTask.ConfigureAwait(false);
                _readTask.Start();

                Logger.Log.Info("Client started...");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("{0}: {1}", nameof(MainViewModel), ex.Message));
            }
        }
        #endregion

        #region methods
        private void InitTask()
        {
            _readTask = new Task(async () =>
            {
                _cancellationToken = new CancellationToken();

                using (var channelFactory = new ChannelFactory<IReadingService>(binding, endpoint))
                {
                    channelFactory.Credentials.UserName.UserName = "test";
                    channelFactory.Credentials.UserName.Password = "test123";

                    IReadingService service = null;
                    try
                    {
                        service = channelFactory.CreateChannel();
                        var reader = await service.GetReaderUsingDataContract(new ReaderType());
                        var reading = await service.GetReadingUsingDataContract(new ReadingType() { ReaderID = reader.ID, StartedDateTime = DateTime.UtcNow });
                        do
                        {
                            var read = await service.GetReadUsingDataContract(new ReadType() { ReadingID = reading.ID });
                            await Task.Delay(Consts.DELAY);
                        } while (_cancellationToken.IsCancellationRequested == false);
                    }
                    catch (Exception ex)
                    {
                        (service as ICommunicationObject)?.Abort();
                        Logger.Log.Error(string.Format("{0}: {1}", nameof(InitTask), ex.Message));
                    }
                }
            });
        }

        private void InitProtocol(ProtocolEnum protocol)
        {
            try
            {
                if (protocol == ProtocolEnum.Http)
                {
                    binding = new WSHttpBinding(SecurityMode.None);
                    endpoint = new EndpointAddress(Consts.HttpLocalhost());
                }
                if (protocol == ProtocolEnum.Tcp)
                {
                    binding = new NetTcpBinding(SecurityMode.None);
                    endpoint = new EndpointAddress(Consts.TcpUrl());
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("{0}: {1}", nameof(InitProtocol), ex.Message));
            }
        }
        #endregion
    }
}