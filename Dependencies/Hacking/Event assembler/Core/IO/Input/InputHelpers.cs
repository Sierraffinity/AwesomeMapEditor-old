using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.IO.Input
{
    public static class InputHelpers
    {
        public static string GetPositionString(this IPositionableInputStream stream)
        {
            return string.Format("File: {0}, Line: {1}",
                Path.GetFileName(stream.CurrentFile),
                stream.LineNumber//, stream.Column
                );
        }

        public static string GetErrorString(this IPositionableInputStream stream, string error)
        {
            return string.Format("{0}: {1}: {2}",
                stream.GetPositionString(),
                error,
                stream.PeekOriginalLine());
        }

        public static string GetErrorString(this IPositionableInputStream stream, CanCauseError error)
        {
            return string.Format("{0}: {1}: {2}",
                stream.GetPositionString(),
                error.ErrorMessage,
                stream.PeekOriginalLine());
        }

        public static string GetErrorString<T>(this IPositionableInputStream stream, CanCauseError<T> error)
        {
            return string.Format("{0}: {1}: {2}",
                stream.GetPositionString(),
                error.ErrorMessage,
                stream.PeekOriginalLine());
        }
    }
}
