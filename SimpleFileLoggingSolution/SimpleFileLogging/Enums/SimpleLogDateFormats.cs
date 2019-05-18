////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Enums\SimpleLogDateFormatTypes.cs
//
// summary:	Implements the simple log date format types class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace SimpleFileLogging.Enums
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Values that represent simple log date format types. </summary>
    ///
    /// <remarks>   Msacli, 17.05.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum SimpleLogDateFormats : byte
    {
        /// <summary>   An enum constant representing the none option. </summary>
        None = 0,

        /// <summary>   An enum constant representing the second option. </summary>
        Second = 10,

        /// <summary>   An enum constant representing the minute option. </summary>
        Minute = 11,

        /// <summary>   An enum constant representing the hour option. </summary>
        Hour = 12,

        /// <summary>   An enum constant representing the day option. </summary>
        Day = 13,

        /// <summary>   An enum constant representing the month option. </summary>
        Month = 14,

        /// <summary>   An enum constant representing the year option. </summary>
        Year = 15
    };
}