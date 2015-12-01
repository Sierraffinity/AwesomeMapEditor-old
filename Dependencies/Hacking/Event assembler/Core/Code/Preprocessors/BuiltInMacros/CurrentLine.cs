using Nintenlord.Event_Assembler.Core.IO.Input;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    class CurrentLine : IMacro
    {
        public IInputStream Stream
        {
            get;
            set;
        }

        #region IBuiltInMacro Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 0;
        }

        public string Replace(string[] parameters)
        {
            return Stream.LineNumber.ToString();
        }

        #endregion

        #region IEquatable<IBuiltInMacro> Members

        public bool Equals(IMacro other)
        {
            return this.GetType() == other.GetType();
        }

        #endregion
    }
}
