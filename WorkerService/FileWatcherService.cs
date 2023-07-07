using EmailService;

namespace WorkerService
{
    public class FileWatcherService : BackgroundService
    {
        private readonly string directoryPath = "C:\\MyDirectory";
        private readonly IEmailService emailService;
        private readonly ILogger<FileWatcherService> logger;

        public FileWatcherService(IEmailService emailService, ILogger<FileWatcherService> logger)
        {
            this.emailService = emailService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var fileSystemWatcher = new FileSystemWatcher(directoryPath))
            {
                fileSystemWatcher.NotifyFilter =
                    NotifyFilters.FileName |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.Size;

                fileSystemWatcher.Created += OnFileChanged;
                fileSystemWatcher.Deleted += OnFileChanged;
                fileSystemWatcher.Changed += OnFileChanged;

                fileSystemWatcher.EnableRaisingEvents = true;

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            string action = "";
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                action = "created";
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                action = "deleted";
            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                action = "modified";
            }

            logger.LogInformation($"File {e.Name} {action}");

            string recipientEmail = "recipient@example.com";
            string subject = $"File {e.Name} {action}";
            string body = $"The file {e.Name} was {action} in the directory {directoryPath}";

            emailService.SendEmail(recipientEmail, subject, body);
        }
    }
}
