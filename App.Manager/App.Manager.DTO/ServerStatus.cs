namespace App.Manager.DTO
{
    public class ServerStatus
    {

        private int _code;

        public int Code
        {
            get { return _code; }
            private set { _code = value; }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            private set { _status = value; }
        }


        public static readonly ServerStatus Starting = new ServerStatus()
        {
            Code = 0,
            Status = "Starting"
        };

        public static readonly ServerStatus Started = new ServerStatus()
        {
            Code = 1,
            Status = "Started"
        };

        public static readonly ServerStatus Stopping = new ServerStatus()
        {
            Code = 2,
            Status = "Stopping"
        };

        public static readonly ServerStatus Stopped = new ServerStatus()
        {
            Code = 3,
            Status = "Stopped"
        };

        public static readonly ServerStatus Pausing = new ServerStatus()
        {
            Code = 4,
            Status = "Pausing"
        };

        public static readonly ServerStatus Paused = new ServerStatus()
        {
            Code = 5,
            Status = "Paused  "
        };

        public static readonly ServerStatus Continuing = new ServerStatus()
        {
            Code = 6,
            Status = "Continuing"
        };


        public static ServerStatus GetStatus(int statusCode)
        {

            if (statusCode == Started.Code)
            {
                return Started;
            }
            else if (statusCode == Starting.Code)
            {
                return Starting;
            }
            else if (statusCode == Stopped.Code)
            {
                return Stopped;
            }
            else if (statusCode == Stopping.Code)
            {
                return Stopping;
            }
            else if (statusCode == Continuing.Code)
            {
                return Continuing;
            }
            else if (statusCode == Pausing.Code)
            {
                return Pausing;
            }
            else
            {
                return Paused;
            }
        }
    }
}