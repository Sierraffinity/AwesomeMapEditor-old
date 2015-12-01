using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nintenlord.Event_Assembler.Core.IO.Logs
{
    public abstract class MessageLog : ILog
    {
        List<string> messages;
        List<string> errors;
        List<string> warnings;

        public MessageLog()
        {
            messages = new List<string>();
            warnings = new List<string>();
            errors = new List<string>();
        }

        protected string GetText(out int longestLine)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Finished.");
            longestLine = 30;
            if (messages.Count > 0)
            {
                message.AppendLine("Messages:");
                foreach (string item in messages)
                {
                    message.AppendLine(item);
                    longestLine = Math.Max(longestLine, item.Length);
                }
                message.AppendLine();
            }
            if (errors.Count > 0)
            {
                message.AppendLine(errors.Count + " errors encountered:");
                foreach (string item in errors)
                {
                    message.AppendLine(item);
                    longestLine = Math.Max(longestLine, item.Length);
                }
                message.AppendLine();
            }
            if (warnings.Count > 0)
            {
                message.AppendLine(warnings.Count + " warnings encountered:");
                foreach (string item in warnings)
                {
                    message.AppendLine(item);
                    longestLine = Math.Max(longestLine, item.Length);
                }
                message.AppendLine();
            }
            if (warnings.Count == 0 && errors.Count == 0)
            {
                message.AppendLine("No errors or warnings.");
                message.AppendLine("Please continue being awesome.");
                message.AppendLine();
            }
            return message.ToString();
        }
        protected string GetText()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Finished.");
            if (messages.Count > 0)
            {
                message.AppendLine("Messages:");
                foreach (string item in messages)
                {
                    message.AppendLine(item);
                }
                message.AppendLine();
            }
            if (errors.Count > 0)
            {
                message.AppendLine(errors.Count + " errors encountered:");
                foreach (string item in errors)
                {
                    message.AppendLine(item);
                }
                message.AppendLine();
            }
            if (warnings.Count > 0)
            {
                message.AppendLine(warnings.Count + " warnings encountered:");
                foreach (string item in warnings)
                {
                    message.AppendLine(item);
                }
                message.AppendLine();
            }
            if (warnings.Count == 0 && errors.Count == 0)
            {
                message.AppendLine("No errors or warnings.");
                message.AppendLine("Please continue being awesome.");
                message.AppendLine();
            }
            return message.ToString();
        }

        #region IMessageLog Members

        public void AddError(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            errors.Add(message);
        }

        public void AddWarning(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            warnings.Add(message);
        }

        public void AddMessage(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            messages.Add(message);
        }

        public void Clear()
        {
            messages.Clear();
            warnings.Clear();
            errors.Clear();
        }
        
        public void AddError(string file, string line, string message)
        {
            this.AddError(Path.GetFileName(file) + ": " + message + " : " + line);
        }

        public void AddWarning(string file, string line, string message)
        {
            this.AddWarning(Path.GetFileName(file) + ": " + message + " : " + line);
        }

        public void AddMessage(string file, string line, string message)
        {
            this.AddMessage(Path.GetFileName(file) + ": " + message + " : " + line);
        }

        public abstract void PrintAll();

        public void AddError(string format, object[] parameters)
        {
            this.AddError(string.Format(format, parameters));
        }

        public void AddWarning(string format, object[] parameters)
        {
            this.AddWarning(string.Format(format, parameters));
        }

        public void AddMessage(string format, object[] parameters)
        {
            this.AddMessage(string.Format(format, parameters));
        }
        
        #endregion
    }
}
