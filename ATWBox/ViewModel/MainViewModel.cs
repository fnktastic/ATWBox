using System;
using GalaSoft.MvvmLight;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows;
using ATWBox.Enum;
using ATWService.Model;
using ATWService;

namespace ATWBox.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region fields
        private Task _readTask;
        private CancellationToken _cancellationToken;
        private dynamic binding;
        private dynamic endpoint;
        #endregion

        #region
        private ObservableCollection<Read> _reads { get; set; } = new ObservableCollection<Read>();
        public ObservableCollection<Read> Reads
        {
            get { return _reads; }
            set { _reads = value; RaisePropertyChanged("Reads"); }
        }
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
                        var reader = await service.SetReaderAsync(new Reader());
                        var reading = await service.SetReadingAsync(new Reading() { ID = Guid.NewGuid(), ReaderID = reader.ID, IPAddress = "192.168.15.125", StartedDateTime = DateTime.UtcNow });
                        do
                        {
                            var read = await service.SetReadAsync(new Read() { ID = Guid.NewGuid(), ReadingID = reading.ID, EPC = "TAG 14" });

                            Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                _reads.Add(read);
                            }));

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
                    endpoint = new EndpointAddress(Consts.HttpUrl());
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