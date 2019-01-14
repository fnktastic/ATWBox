using System;
using System.Runtime.Serialization;

namespace GettingDataService.Contract
{
    [DataContract]
    public class ReadingType
    {
        private int _id;
        private string _antennaNumber;
        private string _readerNumber;
        private string _ipAddress;
        private string _uniqueReadingID;
        private string _timingPoint;
        private int _totalReadings;
        private string _fileName;
        private DateTime? _startedDateTime;
        public DateTime? _endedDateTime;

        private int _readerID;
    }
}