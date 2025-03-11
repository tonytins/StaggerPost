// I hereby waive this project under the public domain - see UNLICENSE for details.
const string banner = "=== Publish Times ===";

var numberOfArticles = 5; // Define how many articles to schedule
var startTime = new TimeSpan(9, 0, 0); // Starting time at 9:00 AM
var random = new Random();
var scheduledTimes = new List<TimeSpan>();

for (int i = 0; i < numberOfArticles; i++)
{
    var baseDelayHours = random.Next(2, 4); // Randomly choose between 2-3 hours delay
    var minutesToAdd = random.Next(0, 60); // Randomly choose minutes (0-59)

    // Calculate new time by adding base delay and random minutes
    var nextTime = startTime.Add(new TimeSpan(baseDelayHours, minutesToAdd, 0));

    // Check if the new time is within 30 minutes of any existing time
    while (scheduledTimes.Exists(previousTime => Math.Abs((nextTime - previousTime).TotalMinutes) < 30))
    {
        // If the new time is within 30 minutes of an existing time, adjust it
        nextTime = nextTime.Add(new TimeSpan(0, 30, 0));
    }

    scheduledTimes.Add(nextTime);
    startTime = nextTime; // Update start time for the next article
}

string TimeSpanToAMPM(TimeSpan time)
{
    var minutes = time.TotalMinutes;
    var hours12 = time.Hours % 12;

    if (hours12 == 0)
        hours12 = 1;

    var period = time.Hours >= 12 ? "PM" : "AM";
    return $"{hours12}:{time.Minutes:D2} {period}";
}

Console.WriteLine(banner);
foreach (var time in scheduledTimes)
{
    // Correct format string to display time in 12-hour format with AM/PM
    Console.WriteLine($"Article {scheduledTimes.IndexOf(time) + 1} Scheduled at: {TimeSpanToAMPM(time)}");
}
