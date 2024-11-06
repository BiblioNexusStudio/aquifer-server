namespace Aquifer.Jobs.Common;

/**
 * The basic format of the CRON expressions in Azure is:
 * {second} {minute} {hour} {day} {month} {day of the week}
 * 
 * Accepted values:
 * {second}	          0-59; *	        {second} when the trigger will be fired
 * {minute}	          0-59; *	        {minute} when the trigger will be fired
 * {hour}	          0-23; *	        {hour} when the trigger will be fired
 * {day}	          1-31; *	        {day} when the trigger will be fired
 * {month}	          1-12; *	        {month} when the trigger will be fired
 * {day of the week}  0-6; SUN-SAT; *	{day of the week} when the trigger will be fired
 */
public static class CronSchedules
{
    public const string EveryMinute = "0 * * * * *";
    public const string EveryFiveMinutes = "0 */5 * * * *";
    public const string EveryTenMinutes = "0 */10 * * * *";
    public const string Hourly = "0 0 * * * *";
    public const string Daily = "0 0 0 * * *";

    public const string EveryHourAtFiveAfter = "0 5 * * * *";
    public const string FirstOfMonthAtNoon = "0 0 12 1 * *";
}